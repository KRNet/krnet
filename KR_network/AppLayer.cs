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
        public AppLayer(PhysicalLayer physicalLayer)
        {
            this.messageQueue = new ConcurrentQueue<Msg>();
            new Thread(ReadFromDLL).Start();
            this.countToSend = 0;
        }

        public void SendInfoMessage(string msg)
        {
            Msg message = new Msg(msg);
            messageQueue.Enqueue(message);
        }

        public void SendManageMessage(Msg.ManageType type)
        {
            Msg message = new Msg(type);
            messageQueue.Enqueue(message);
        }

        public void DeleteSentMessage()
        {
            Msg msg;
            messageQueue.TryDequeue(out msg);
            this.countToSend = 0;
        }

        public void SendToDLL()
        {
            const int MAX_TRIES = 3;
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
                        if (previousMsg != null && previousMsg.Equals(msg))
                            tries++;
                        if (tries == MAX_TRIES)
                        {
                            //добавить в форму сообщение о разъединении
                            Data.physicalLayer.closeConnection();
                        }
                        previousMsg = msg;
                    }
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
                    this.systemQueue.TryDequeue(out msg);
                    Data.dll.sendMessage(msg.toString());
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
                    switch (msg.getType())
                    {
                        case Msg.Types.info:
                            //нужно вывести в форму
                            SendManageMessage(Msg.ManageType.ACK);//послали подтверждение
                            break;
                        case Msg.Types.manage:
                            switch (msg.getManageType())
                            {
                                case Msg.ManageType.REQUEST_CONNECT:
                                    SendManageMessage(Msg.ManageType.CONNECT);
                                    //вывести сообщение о создании соединения
                                    break;

                                case Msg.ManageType.REQUEST_DISCONNECT:
                                    SendManageMessage(Msg.ManageType.DISCONNECT);
                                    //вывести сообщение о закрытии соединения
                                    break;

                                case Msg.ManageType.ACK:
                                    DeleteSentMessage();
                                    break;
                                
                                case Msg.ManageType.DISCONNECT:
                                    Data.physicalLayer.closeConnection();
                                    //вывести сообщение о закрытии соединения
                                    break;

                                case Msg.ManageType.CONNECT:
                                    //вывести сообщение о создании соединения соединения,
                                    //разблокировать формы ввода
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
