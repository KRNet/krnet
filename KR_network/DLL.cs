using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace KR_network
{
    //public static enum frameType { INFO = 1, ACK = 2, RET = 3};
    class DLL
    {
        private Thread threadFromAppLayer;
        private Thread threadFromPhysicalLayer;
        private Thread threadSendFrames;

        private ConcurrentQueue<String> dataFromAppLayer;
        private ConcurrentQueue<Frame> framesToSend;

        private Boolean frameWasSended; //Контролирует очередь кадров на отправку
        private int countToSend;    //Контролирует очередь кадров на отправку

        private PhysicalLayer physicalLayer;
        private Byte stopByte = 254;
        private Byte startByte = 253;


        private List<byte> byteBuffer;
        private LinkedList<Frame> frameBuffer;
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
                    addBytes(received);
                }
                Thread.Sleep(200);
            }
        
        }

        //Служба передачи кадров
        private void sendFrames()
        {
            while (true)
            {
                if (this.physicalLayer.connectionActive)
                {
                    if ((!frameWasSended || countToSend == 0) && (!framesToSend.IsEmpty))
                    {
                        Frame frame;
                        this.frameWasSended = true;
                        framesToSend.TryPeek(out frame);
                        physicalLayer.sendFrame(frame.serialize());
                        framesToSend.TryDequeue(out frame);
                        countToSend = 5;
                    }
                    countToSend -= 1;   
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
        
        public void addBytes(byte[] b)
        {
            Array byteArray = Array.CreateInstance(typeof(byte), (long)b.Count());
            for (int i = 0; i < b.Count(); i++)
            {
                if (this.byteBuffer.Count == 0)
                {
                    if (b[i] == startByte)
                    {
                        this.byteBuffer.Add(b[i]);
                    }
                }
                else
                {
                    if (b[i] == startByte)
                    {
                        this.byteBuffer.Clear();
                        this.byteBuffer.Add(b[i]);
                    }
                    else if (b[i] == stopByte)
                    {
                        this.byteBuffer.Add(b[i]);
                        Frame newFrame = Frame.deserialize(this.byteBuffer.ToArray());
                        if (newFrame.isInformationFrame())
                        {
                            if (!newFrame.damaged())
                            {
                                Frame ackFrame = new Frame(new byte[0], 2);
                                physicalLayer.sendFrame(ackFrame.serialize());
                                Console.Write(getString(newFrame.getData()));
                                this.stringsBuffer.Enqueue(
                                                            getString(newFrame.getData())
                                                        );
                            }
                            else
                            {
                                Frame retFrame = new Frame(new byte[0], 3);
                                physicalLayer.sendFrame(retFrame.serialize());
                            }
                            
                        }
                        else
                        {
                            processControlFrame(newFrame);
                        }
                    }
                    else
                    {
                        this.byteBuffer.Add(b[i]);
                    }
                    
                }
            }

        }

        public void clearByteBuffer()
        {
            this.byteBuffer = null;
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
        
    }
}
