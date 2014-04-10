using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR_network
{   
    class Frame
    {
        private byte startByte;
        private byte stopByte;
        private byte type;
        private byte lengthOfData;
        private byte[] data;
        private int frameLength;

        public Frame(byte[] data, byte type)
        {
            this.startByte = 1; //Стартовый байт = 1
            this.stopByte = 2;
            this.type = type;   //type = 1 для информационных кадров
            this.frameLength = 3;
            if (type == 1)
            {
                //Нужно вызывать кодирование сначала
                this.lengthOfData = (byte)data.Length;
                this.data = data;
                this.frameLength += lengthOfData + 1;
            }
        }

        public byte[] serialize()
        {
            byte[] serialized = new byte[frameLength];
            serialized.ToList().Add(startByte);
            serialized.ToList().Add(type);
            if (type == 1)
            {
                serialized.ToList().Add(lengthOfData);
                foreach (var b in data) { serialized.ToList().Add(b); }
            }
            serialized.ToList().Add(stopByte);
            return serialized;
        }

        static public Frame deserialize(byte[] array)
        {
            byte type = array.ElementAt(1);
            byte[] dataFromArray = new byte[0];
            if (type == 1)  //Если кадр информационный
            {
                byte length = array.ElementAt(2);
                dataFromArray = new byte[length];
                for (byte i = 3; i < 3 + length; i++)
                    dataFromArray.ToList().Add(array[i]);
            }
            return new Frame(dataFromArray, type);

        }

        public void final(byte[] data)
        {

        }

       /* public byte[] getFinal() //FIXED
        {
          
        }
        */
        public static byte[] cycle(byte b)
        {
            int iVector = Convert.ToInt32(b);
            iVector = iVector << 4;

            BitArray porozhBitArr = new BitArray(new bool[] { true, false, false, true, true });
            int porozh = 19;

            int remainderOfDividing = divide(iVector, porozh);

            iVector += remainderOfDividing;

            byte[] bytes = BitConverter.GetBytes(iVector).Reverse().ToArray();
            return (new byte[] { bytes[2], bytes[3] });
        }
        public static int divide(int iVector, int porozh) //потестить отдельно. Приходит 8 бит, возвращается 15 бит
        {
            BitArray iVectorBitArr = new BitArray(new int[] { iVector });
            BitArray porozhBitArr = new BitArray(new int[] { porozh });


            int initialLenght = getLengthOfNumber(porozh);

            int sdvig = getLengthOfNumber(iVector) - getLengthOfNumber(porozh);
            porozh = porozh << sdvig; //Count - можно только get, Length - еще и set
            while (getLengthOfNumber(iVector) >= initialLenght)
            {
                iVector = iVector ^ porozh;
                int tmp = getLengthOfNumber(porozh) - getLengthOfNumber(iVector);
                tmp = (tmp < 0) ? 0 : tmp;
                porozh = porozh >> tmp;
                Console.WriteLine("a");
            }
            return iVector;
        }

        public static int getLengthOfNumber(int number)
        {
            bool[] numberBoolArr = new BitArray(new int[] { number }).Cast<bool>().Reverse().ToArray();
            String numberString = "";
            foreach (var item in numberBoolArr)
            {
                numberString += (item) ? 1 : 0;
            }
            numberString = numberString.TrimStart('0');
            return numberString.Length;
        }


        public static void decycle(byte[] b)
        {
            byte[] toConvert = new byte[] { 0, 0, b[0], b[1] };
            int inputInt = BitConverter.ToInt32(toConvert.Reverse().ToArray(), 0);
            int syndrom = divide(inputInt, 19);

            Console.WriteLine("syndrom: ", syndrom);
        }

        public byte[] encode(byte[] bytes)
        {
            int length = bytes.Count() * 2;
            byte[] result = new byte[length * 2];
            for (int i = 0; i < length; i++)
            {
                byte[] cycled = cycle(bytes[i]);
                result[i * 2] = cycled[0];
                result[i * 2 + 1] = cycled[1];
            }
            return result;
        }

        public void decode(byte[] b)
        {

        }

    }
}
