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
        private int countToSend;
        Dialog dialogForm;
        private int triesInfo = 0;
        private int countForApproving = 0;
        private bool waitingApprove = false;
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
            this.countToSend = 0;
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
            dialogForm.info_text.Text = "Соединение закрыто";
            systemQueue = new ConcurrentQueue<Msg>();
            messageQueue = new ConcurrentQueue<Msg>();
            Data.physicalLayer.closeConnection();
            Data.dll.closeThreads();
            input.Abort();
            outputInfo.Abort();
            outputSystem.Abort();
        }

        public void SendInfoMessage(string msg)
        {
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
            this.countToSend = 0;
            this.triesInfo = 0;
        }

        public void SendToDLL()
        {
            const int MAX_TRIES = 2;
            Msg previousMsg = null;
            bool firstTime = true;
            while (true)
            {
                if (countToSend-- <= 0)
                {
                    if (!this.messageQueue.IsEmpty)
                    {
                        Msg msg;
                        messageQueue.TryPeek(out msg);

                        if (firstTime)
                        {
                            previousMsg = msg;
                            firstTime = false;
                        }
                        Data.dll.sendMessage(msg.toString());
                        dialogForm.info_text.Text = "Выполняется передача";
                        if (previousMsg != null && previousMsg.Equals(msg))
                            triesInfo++;
                        if (triesInfo == MAX_TRIES)
                        {
                            closeConnection("Закрытие соединения. Проблема с сетью.");
                        }
                        previousMsg = msg;
                    }
                    else
                        if (!firstTime)
                            dialogForm.info_text.Text = "Все сообщения отправлены";
                    countToSend = 10;
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
                Thread.Sleep(200);
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
                                    dialogForm.info_text.Text = "Соединение не установлено";
                                    break;

                                case Msg.ManageType.ACK:
                                    DeleteSentMessage();
                                    break;

                                case Msg.ManageType.DISCONNECT:
                                    if (this.waitingApprove == false)
                                        Console.WriteLine("fuck");
                                    this.waitingApprove = false;
                                    closeConnection("Соединение закрыто");
                                    dialogForm.exit();
                                    break;

                                case Msg.ManageType.CONNECT:
                                    if (this.waitingApprove == false)
                                        Console.WriteLine("fuck");
                                    this.waitingApprove = false;
                                    dialogForm.writeSystemMessage("Соединение установлено");
                                    dialogForm.sendBtn.Enabled = true;
                                    dialogForm.richTextBox1.Enabled = true;
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