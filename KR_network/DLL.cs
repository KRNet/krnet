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

        private List<byte> chunk = new List<byte>(); 

        public DLL(PhysicalLayer physicalLayer)
        {
            threadFromAppLayer = new Thread(readFromAppLayer);
            threadFromAppLayer.Start();
            threadFromPhysicalLayer = new Thread(readFromPhLayer);
            threadFromPhysicalLayer.Start();
            this.physicalLayer = physicalLayer;
        }

        public void readFromPhLayer()
        {
            while (true)
            {
                byte[] dataReceived = physicalLayer.getAllFromDllBuffer();
                List<byte> bufferForFrame = new List<byte>();

                
                for (int i = 0; i < dataReceived.Count(); i++)
                {
                    
                    if (dataReceived[i] == startByte)
                    {
                        i++;
                        for (; dataReceived[i] != stopByte && i < dataReceived.Count(); i++)
                            bufferForFrame.Add(dataReceived[i]);
                    }
                    else {
                        if (chunk.Count() != 0)
                        {
                            ///добавить в чанк
                        }
                        else
                        {
                            //создать чанк
                        }
                    }
                    bufferForFrame = new List<byte>();
                }

                Thread.Sleep(200);
            }
        }

        public void readFromAppLayer()
        {
            while (true)
            {
                lock(dataFromAppLayer)
                {
                    string message;
                    if (dataFromAppLayer.TryDequeue(out message))
                    {
                        Frame frame = makeFrame(message);
                        physicalLayer.sendFrame(frame.getFinal());
                    }

                }
                Thread.Sleep(200);
            }
        }


        public void sendToDLL(string data)
        {
            lock (dataFromAppLayer)
            {   
                dataFromAppLayer.Add(data);
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

        public Frame makeFrame(String data)
        {
            return new Frame(getBytes(data)); // сделать конструктор
        }

    }
}
