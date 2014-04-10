using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace KR_network
{
    class DLL
    {
        private Thread threadFromAppLayer;
        private Thread threadFromPhysicalLayer;
        private ConcurrentQueue<String> dataFromAppLayer;
        private PhysicalLayer physicalLayer;
        private Byte stopByte = 2;
        private Byte startByte = 1;

        private List<byte> byteBuffer;
        private LinkedList<Frame> frameBuffer;
        private ConcurrentQueue<String> stringsBuffer;

        private List<byte> chunk = new List<byte>();

        public DLL(PhysicalLayer physicalLayer)
        {
            stringsBuffer = new ConcurrentQueue<string>();
            threadFromPhysicalLayer = new Thread(readFromPhLayer);
            threadFromPhysicalLayer.Start();
            this.physicalLayer = physicalLayer;
        }

        //Служба чтения с физического уровня
        private void readFromPhLayer()
        {
            while (physicalLayer.connectionActive)
            {
                byte[] received = physicalLayer.getAllFromDllBuffer();
                addBytes(received);
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

        public void sendMessage(String data)
        {
            Frame frame = makeFrame(data);
            physicalLayer.sendFrame(frame.serialize());
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
                            this.stringsBuffer.Enqueue(
                                                        getString(newFrame.getData())
                                                    );
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
            Console.WriteLine("processControlFrame");
        }
    }
}
