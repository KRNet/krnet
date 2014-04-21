using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace KR_network
{
    /*
     * Посмотрим, что у нас здесь...
     * Канальный уровень передатчика ждет, пока канальный уровень получателя
     * считает предыдущий посланный кадр.
     * Когда получатель считал кадр, он считается доступным и передатчик может снова слать кадры.
     * При этом, если получатель получил кадр, но по каким-либо причинам передатчик не получил ACK, 
     * он будет перепосылать тот же кадр по тайм-ауту. Но если он получил ACK, а приемник не доступен,
     * передатчик будет ждать доступности (physicalLayer.readyToSend)
     */

    class DLL
    {
        private Thread threadFromPhysicalLayer;
        private Thread threadSendFrames;
        private Thread threadSendLinkFrame;

        private ConcurrentQueue<Frame> framesToSend;

        private Boolean frameWasSended; //Контролирует очередь кадров на отправку
        private int countToSend;    //Контролирует очередь кадров на отправку

        private PhysicalLayer physicalLayer;
        private Byte stopByte = Data.STOPByte;
        private Byte startByte = Data.STARTByte;

        private bool canSend;
        private bool linkStatus;
        private int linkFramesSended;

        private ConcurrentQueue<String> stringsBuffer;

        public DLL(PhysicalLayer physicalLayer)
        {
            this.physicalLayer = physicalLayer;
            frameWasSended = false;
            linkStatus = true;
            linkFramesSended = 0;
            canSend = true;
            countToSend = Data.DLLSendTimeout;    //5 циклов по 200мс
            framesToSend = new ConcurrentQueue<Frame>();
            stringsBuffer = new ConcurrentQueue<string>();
            threadFromPhysicalLayer = new Thread(readFromPhLayer);
            threadFromPhysicalLayer.Start();
            threadSendFrames = new Thread(sendFrames);
            threadSendFrames.Start();
            threadSendLinkFrame = new Thread(sendLinkFrame);
            threadSendLinkFrame.Start();
        }

        public bool getLinkStatus()
        {
            return linkStatus;
        }

        public void closeThreads()
        {
            threadFromPhysicalLayer.Abort();
            threadSendFrames.Abort();
            threadSendLinkFrame.Abort();
        }

        //Служба чтения с физического уровня
        private void readFromPhLayer()
        {
            while (true)
            {
                if (this.physicalLayer.connectionActive)
                {
                    byte[] received = physicalLayer.getAllFromDllBuffer();
                    if (received.Length != 0) 
                        process(received);
                }
                Thread.Sleep(Data.DLLReadFromPLTimeout);
            }
        }

        //Служба передачи кадров (см. описание сверху)
        private void sendFrames()
        {
            while (true)
            {
                if (this.physicalLayer.connectionActive && this.canSend)
                {
                    if (frameWasSended)
                        Console.WriteLine("ACK не пришел");
                    if (!frameWasSended || countToSend <= 0)
                    {
                        lock (framesToSend)
                        {
                            if (!framesToSend.IsEmpty && physicalLayer.readyToSend())
                            {
                                if (countToSend == 0)
                                    Console.WriteLine("Sending frame because timeout");
                                else
                                    Console.WriteLine("Sending next frame");
                                Frame frame;
                                this.frameWasSended = true;
                                framesToSend.TryPeek(out frame);
                                Console.WriteLine();
                                Console.Write(DateTime.Now.ToString());
                                Console.WriteLine("КАНАЛЬНЫЙ ПОСЛАЛ НА ФИЗИЧЕСКИЙ");
                                physicalLayer.sendFrame(frame.serialize());
                                countToSend = Data.DLLSendTimeout;
                            }
                        }
                    }
                    else
                    {
                        if (frameWasSended)
                        {
                            countToSend -= 1;
                        }
                    }
  
                }
                Thread.Sleep(Data.DLLSendRepeatTime);
            }
            
        }

        private void sendLinkFrame()
        {
            while (true)
            {
                if (physicalLayer.connectionActive)
                {
                    if (linkFramesSended >= 3)
                    {
                        linkStatus = false;
                        return;
                    }
                    else
                    {
                        Frame LinkFrame = new Frame(new byte[0], Data.LINKFrame);
                        stopSendingFrames();
                        Console.WriteLine("Sending LINK...");
                        physicalLayer.sendFrame(LinkFrame.serialize());
                        continueSendingFrames();

                        linkFramesSended += 1;
                    }
                    
                    
                }
                Thread.Sleep(Data.LinkFrameTimeout);
            }
        }

        //Метод для прикладного уровня
        public String readFromDLLBuffer()
        {
            if (stringsBuffer.IsEmpty)
                return "";
            else
            {
                Console.WriteLine();
                Console.Write(DateTime.Now.ToString());
                Console.WriteLine("ПРИКЛАДНОЙ СЧИТАЛ С КАНАЛЬНОГО");
                String msg;
                stringsBuffer.TryDequeue(out msg);
                return msg;
            }
        }

        static byte[] getBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string getString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public Frame makeInformationFrame(String data)
        {
            byte type = Data.INFOFrame; //Информационный кадр
            return new Frame(getBytes(data), type); // сделать конструктор
        }

        //Метод для прикладного уровня
        public void sendMessage(String data)
        {
            lock (framesToSend)
            {
                Frame frame = makeInformationFrame(data);
                framesToSend.Enqueue(frame);
            }
        }

        public void process(byte[] b)
        {
            byte typeOut = 0;
            Frame receivedFrame = Frame.deserialize(b, out typeOut);
            if (receivedFrame == null && typeOut == 1)
            {
                sendRetryFrame();
            }
            else if(receivedFrame != null)
            {
                if (receivedFrame.isInformationFrame())
                {
                    processInfoFrame(receivedFrame);
                }
                else
                {
                    processControlFrame(receivedFrame);
                }
            }
            
        }

        //Обработка системного кадра
        private void processControlFrame(Frame frame)
        {
            //Если пришел ACK, то удалять из очереди и ставить флаг wasSended = false
            if (frame.getType() == Data.ACKFrame)
            {
                lock (framesToSend)
                {
                    Frame pulled = null;
                    framesToSend.TryDequeue(out pulled);
                }
                Console.WriteLine("ACK received");
                this.frameWasSended = false;
                
            }
            //Если пришел RET, то ставить флаг wasSended = false;
            if (frame.getType() == Data.RETFrame)
            {
                this.frameWasSended = false;
            }

            if (frame.getType() == Data.LINKFrame)
            {
                sendLinkAckFrame();
            }

            if (frame.getType() == Data.LINK_ACKFrame)
            {
                linkStatus = true;
                linkFramesSended = 0;
            }
        }

        //Обработка информационного кадра
        private void processInfoFrame(Frame frame)
        {
                Frame ackFrame = new Frame(new byte[0], Data.ACKFrame);


                Console.WriteLine(getString(frame.getData()));
        

                stopSendingFrames();
                Console.WriteLine("Sending ACK...");
                physicalLayer.sendFrame(ackFrame.serialize());
                continueSendingFrames();

                this.stringsBuffer.Enqueue(getString(frame.getData()));

        }

        private void sendRetryFrame()
        {
            Frame retFrame = new Frame(new byte[0], Data.RETFrame);
            stopSendingFrames();
            Console.WriteLine("Sending RET...");
            physicalLayer.sendFrame(retFrame.serialize());
            continueSendingFrames();
        }

        private void sendLinkAckFrame()
        {
            Frame LinkACKFrame = new Frame(new byte[0], Data.LINK_ACKFrame);
            stopSendingFrames();
            Console.WriteLine("Sending LINK_ACK...");
            physicalLayer.sendFrame(LinkACKFrame.serialize());
            continueSendingFrames();
        }

        private void stopSendingFrames()
        {
            this.canSend = false;
            Console.WriteLine("Sending stopped");
            while (!physicalLayer.readyToSend());
            Console.WriteLine("Ready to send control frame");
        }

        private void continueSendingFrames()
        {
            Console.WriteLine("Trying start sending");
            //while (!physicalLayer.readyToSend());
            this.canSend = true;
            Console.WriteLine("Sending started");
        }
        
    }
}
