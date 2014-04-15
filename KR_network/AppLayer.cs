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
        
        public AppLayer(PhysicalLayer physicalLayer)
        {
            this.messageQueue = new ConcurrentQueue<Msg>();
            this.systemQueue = new ConcurrentQueue<Msg>();
            new Thread(ReadFromDLL).Start();
            new Thread(SendToDLL).Start();
            new Thread(SendManageToDLL).Start();
            this.countToSend = 0;
        }

        public void setForm(Dialog dialog)
        {
            this.dialogForm = dialog;
        }

        public void closeConnection(String message)
        {
            dialogForm.messages.Items.Add(message);
            dialogForm.sendBtn.Enabled = false;
            dialogForm.richTextBox1.Enabled = false;
            dialogForm.info_text.Text = "Соединение закрыто";
            systemQueue = new ConcurrentQueue<Msg>();
            messageQueue = new ConcurrentQueue<Msg>();
            Data.physicalLayer.closeConnection();
        }

        public void SendInfoMessage(string msg)
        {
            Msg message = new Msg(msg);
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
        }

        public void SendToDLL()
        {
            const int MAX_TRIES = 2;
            Msg previousMsg = null;
            int tries = 0;
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
                            tries++;
                        if (tries == MAX_TRIES)
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
                if (!systemQueue.IsEmpty)
                {
                    Msg msg;
                    while(!this.systemQueue.TryDequeue(out msg));
                    Data.dll.sendMessage(msg.toString());
                    dialogForm.messages.Items.Add("sended " + msg.toString());
                }
                Thread.Sleep(100);
            }
        }
        
        public void ReadFromDLL()
        {
            while (true)
            {
                string message = Data.dll.readFromDLLBuffer();
                if (!message.Equals(""))
                {
                    Msg msg = Msg.toMsg(message);
                    dialogForm.messages.Items.Add("recieved  " + msg.toString());
             
                    switch (msg.getType())
                    {
                        case Msg.Types.info:
                            dialogForm.messages.Items.Add(msg.getMessage());
                            SendManageMessage(Msg.ManageType.ACK);
                            break;
                        case Msg.Types.manage:
                            switch (msg.getManageType())
                            {
                                case Msg.ManageType.REQUEST_CONNECT:
                                    SendManageMessage(Msg.ManageType.CONNECT);
                                    break;

                                case Msg.ManageType.REQUEST_DISCONNECT:
                                    Data.dll.sendMessage((new Msg(Msg.ManageType.DISCONNECT)).toString());// сразу послать, потом закрыть порт
                                    Thread.Sleep(1500);//на всякий
                                    closeConnection("Собеседник закрыл соединение");
                                    dialogForm.info_text.Text = "Соединение не установлено";
                                    break;

                                case Msg.ManageType.ACK:
                                    DeleteSentMessage();
                                    break;

                                case Msg.ManageType.DISCONNECT:
                                    closeConnection("Соединение закрыто");
                                    dialogForm.Close();
                                    dialogForm.getParent().Show();
                                    break;

                                case Msg.ManageType.CONNECT:
                                    dialogForm.messages.Items.Add("Соединение установлено");
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
