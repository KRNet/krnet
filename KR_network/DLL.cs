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


        private LinkedList<byte> byteBuffer;
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
                    else
                    {
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

     /*   public void addBytes(byte[] b)
        {
            if (this.byteBuffer.Count != 0)
            {
                Array byteArray = Array.CreateInstance(typeof(byte), (long)b.Count());
                if (Array.IndexOf(byteArray, stopByte) != -1
                    && Array.IndexOf(byteArray, stopByte) < Array.IndexOf(byteArray, stopByte)
                    )
                {
                    
                    int stopByteIndex = Array.IndexOf(byteArray, stopByte);

                    System.Array.Copy(b, )
                    b
                }
            }
        }
        */
    }
}
