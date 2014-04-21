using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Windows.Forms;
using System.Threading;

namespace KR_network
{
    class AppLayer
    {
        private ConcurrentQueue<Msg> messageQueue;
        private ConcurrentQueue<Msg> systemQueue;
        Dialog dialogForm;
        private int countForApproving = 0;
        private int countForACK = 0;
        private bool waitingApprove = false;
        private bool waitingACK = false;
        private string nickname;
        private Thread outputSystem;
        private Thread outputInfo;
        private Thread input;

        public AppLayer(PhysicalLayer physicalLayer, string nickname)
        {
            this.messageQueue = new ConcurrentQueue<Msg>();
            this.systemQueue = new ConcurrentQueue<Msg>();
            input = new Thread(ReadFromDLL);
            outputInfo = new Thread(SendToDLL);
            outputSystem = new Thread(SendManageToDLL);
            input.Start();
            outputInfo.Start();
            outputSystem.Start();
            this.nickname = nickname;
        }

        public void setForm(Dialog dialog)
        {
            this.dialogForm = dialog;
        }

        public string getNickname()
        {
            return this.nickname;
        }

        public void closeConnection(String message)
        {
            dialogForm.writeSystemMessage(message);
            dialogForm.sendBtn.Enabled = false;
            dialogForm.richTextBox1.Enabled = false;
            setInfoText("Соединение закрыто");
            systemQueue = new ConcurrentQueue<Msg>();
            messageQueue = new ConcurrentQueue<Msg>();
            Data.physicalLayer.closeConnection();
            Data.dll.closeThreads();
            input.Abort();
            outputInfo.Abort();
            outputSystem.Abort();
        }

        private void setInfoText(string str)
        {
            if (dialogForm != null)
                dialogForm.info_text.Text = str;
        }

        public void SendInfoMessage(string msg)
        {
            Console.WriteLine();
            Console.Write(DateTime.Now.ToString());
            Console.WriteLine("ПРИКЛАДНОЙ ДОБАВИЛ В ОЧЕРЕДЬ");
            Msg message = new Msg(this.nickname, msg);
            messageQueue.Enqueue(message);
        }

        public void SendManageMessage(Msg.ManageType type)
        {
            Msg message = new Msg(type);
            systemQueue.Enqueue(message);
        }

        public void DeleteSentMessage()
        {
            Msg msg;
            messageQueue.TryDequeue(out msg);
            this.countForACK = 50;
            this.waitingACK = false; 
        }

        public void SendToDLL()
        {
            while (true)
            {
                if (!messageQueue.IsEmpty && !waitingACK)
                {
                    Msg msg;
                    messageQueue.TryPeek(out msg);
                    Console.WriteLine();
                    Console.Write(DateTime.Now.ToString());
                    Console.WriteLine("ПРИКЛАДНОЙ ПОСЛАЛ НА КАНАЛЬНЫЙ");
                    Data.dll.sendMessage(msg.toString());
                    waitingACK = true;
                    countForACK = 50;
                    setInfoText("Выполняется передача");
                }
                else
                {
                    if (waitingACK)
                    {
                        setInfoText("Ожидание подтверждения");
                        countForACK--;
                        if (countForACK <= 0)
                        {
                            closeConnection("Закрытие соединения. Проблема с сетью.");
                            waitingACK = false;
                            countForACK = 50;
                        }

                    }
                    else
                    {
                        setInfoText("Сообщения переданы");
                    }

                }
                Thread.Sleep(100);
            }
        }

        public void SendManageToDLL()
        {
            while (true)
            {
                if (this.waitingApprove && this.countForApproving-- <= 0)
                {
                    closeConnection("Закрытие соединения. Проблема с сетью.");
                    this.waitingApprove = false;
                }

                if (!this.systemQueue.IsEmpty)
                {
                    Msg msg = null;
                    systemQueue.TryDequeue(out msg);
                    Data.dll.sendMessage(msg.toString());
                    if (msg.getManageType() == Msg.ManageType.REQUEST_CONNECT || msg.getManageType() == Msg.ManageType.REQUEST_DISCONNECT)
                    {
                        this.waitingApprove = true;
                        this.countForApproving = 50;
                    }
                }
                Thread.Sleep(100);
            }
        }

        public void ReadFromDLL()
        {
            while (true)
            {
                string message = Data.dll.readFromDLLBuffer();
                if (message != null && !message.Equals(""))
                {
                    Msg msg = Msg.toMsg(message);
                    switch (msg.getType())
                    {
                        case Msg.Types.info:
                            Console.WriteLine();
                            Console.Write(DateTime.Now.ToString());
                            Console.WriteLine("ПРИКЛАДНОЙ ПОЛУЧИЛ");
                            dialogForm.writeMessage(msg.getNickname(), msg.getMessage());
                            SendManageMessage(Msg.ManageType.ACK);
                            break;
                        case Msg.Types.manage:
                            switch (msg.getManageType())
                            {
                                case Msg.ManageType.REQUEST_CONNECT:
                                    SendManageMessage(Msg.ManageType.CONNECT);
                                    break;

                                case Msg.ManageType.REQUEST_DISCONNECT:
                                    SendManageMessage(Msg.ManageType.DISCONNECT);
                                    Thread.Sleep(1500);//на всякий
                                    closeConnection("Собеседник закрыл соединение");
                                    setInfoText("Соединение не установлено");
                                    break;

                                case Msg.ManageType.ACK:
                                    DeleteSentMessage();
                                    break;

                                case Msg.ManageType.DISCONNECT:
                                    if (this.waitingApprove)
                                    {
                                        this.waitingApprove = false;
                                        closeConnection("Соединение закрыто");
                                        dialogForm.exit();
                                    }
                                    break;

                                case Msg.ManageType.CONNECT:
                                    if (this.waitingApprove)
                                    {
                                        this.waitingApprove = false;
                                        dialogForm.writeSystemMessage("Соединение установлено");
                                        dialogForm.sendBtn.Enabled = true;
                                        dialogForm.richTextBox1.Enabled = true;
                                    }
                                    break;
                            }
                            break;
                    }
                }
                Thread.Sleep(200);
            }
        }
    }
}