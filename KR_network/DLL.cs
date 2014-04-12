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

        private ConcurrentQueue<Frame> framesToSend;

        private Boolean frameWasSended; //Контролирует очередь кадров на отправку
        private int countToSend;    //Контролирует очередь кадров на отправку

        private PhysicalLayer physicalLayer;
        private Byte stopByte = 254;
        private Byte startByte = 253;
       
        private List<byte> byteBuffer;
        private ConcurrentQueue<String> stringsBuffer;

        private List<byte> chunk = new List<byte>();

        public DLL(PhysicalLayer physicalLayer)
        {
            this.physicalLayer = physicalLayer;
            frameWasSended = false;
            countToSend = 5;    //5 циклов по 200мс
            framesToSend = new ConcurrentQueue<Frame>();
            byteBuffer = new List<byte>();
            stringsBuffer = new ConcurrentQueue<string>();
            threadFromPhysicalLayer = new Thread(readFromPhLayer);
            threadFromPhysicalLayer.Start();
            threadSendFrames = new Thread(sendFrames);
            threadSendFrames.Start();

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
                Thread.Sleep(200);
            }
        }

        //Служба передачи кадров (см. описание сверху)
        private void sendFrames()
        {
            while (true)
            {
                if (this.physicalLayer.connectionActive)
                {
                    if (frameWasSended)
                        Console.WriteLine("ACK не пришел");
                    if (!frameWasSended || countToSend == 0)
                    {
                        if (!framesToSend.IsEmpty && physicalLayer.readyToSend())
                        {
                            Console.WriteLine("Sending frame");
                            Frame frame;
                            this.frameWasSended = true;
                            framesToSend.TryPeek(out frame);
                            physicalLayer.sendFrame(frame.serialize());
                            countToSend = 5;
                        }
                    }
                    else
                    {
                        countToSend -= 1; 
                    }
                      
                }
                Thread.Sleep(200);
            }
            
        }

        //Метод для прикладного уровня
        public String readFromDLLBuffer()
        {
            String msg;
            if (stringsBuffer.IsEmpty)
                return "";
            stringsBuffer.TryDequeue(out msg);
            return msg;
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

        public Frame makeFrame(String data)
        {
            byte type = 1; //Информационный кадр
            return new Frame(getBytes(data), type); // сделать конструктор
        }

        //Метод для прикладного уровня
        public void sendMessage(String data)
        {
            Frame frame = makeFrame(data);
            framesToSend.Enqueue(frame);
        }

        public void process(byte[] b)
        {
            Frame receivedFrame = Frame.deserialize(b);
            if (receivedFrame.isInformationFrame()){
                processInfoFrame(receivedFrame);
            }
            else
            {
                processControlFrame(receivedFrame);
            }
        }

        private void processControlFrame(Frame frame)
        {
            //Если пришел ACK, то удалять из очереди и ставить флаг wasSended = false;
            if (frame.getType() == 2)
            {
                Console.WriteLine("ACK received");
                this.frameWasSended = false;
                Frame pulled = null;
                framesToSend.TryDequeue(out pulled);
            }
            //Если пришел RET, то ставить флаг wasSended = false;
            if (frame.getType() == 3)
            {
                this.frameWasSended = false;
            }
        }

        private void processInfoFrame(Frame frame)
        {
            if (!frame.damaged())
            {
                Frame ackFrame = new Frame(new byte[0], 2);

                Console.WriteLine(getString(frame.getData()));
                Console.WriteLine("Sending ACK...");

                physicalLayer.sendFrame(ackFrame.serialize());

                this.stringsBuffer.Enqueue(getString(frame.getData()));
            }
            else
            {
                Frame retFrame = new Frame(new byte[0], 3);
                physicalLayer.sendFrame(retFrame.serialize());
            }
        }
        
    }
}
