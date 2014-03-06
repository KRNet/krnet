using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace KR_network
{
    /*
     * Теория вкратце:
     * При подключении каждый компьютер выставляет сигнал DTR
     * При этом собеседник выставляет у себя сигнал DSR, то есть он готов к работе
     * При передаче компьютер выставляет запрос RTS
     * Если все хорошо получается ответ CTS (CTSHolding)
     */
    class PhysicalLayer
    {
        public enum _Parity {Even, Mark, None, Odd, Space};
        private Boolean connectionActive = false;
        public SerialPort port;
        public int received = 0;
        //private Thread reading;

        //Полный конструктор
        public PhysicalLayer(string portName, int baudRate, int parity, int dataBits, double stopBits)
        {
            try
            {
                port = new SerialPort(portName, baudRate, parityConvert(parity), 
                    dataBits, stopBitsConvert(stopBits));
                makeActive();
                port.ReadTimeout = 500;
                port.WriteTimeout = 500;
                port.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
                //reading = new Thread(readThread);
                //reading.Start();
            }
            catch (InvalidOperationException) { }
        }

        //Проверяет доступность порта
        public static Boolean isActivePort(String portName)
        {
            List<String> portsAvailable = searchPorts();
            if (portsAvailable.Contains(portName))
                return true;
            return false;
        }

        //Получает список доступных портов
        public static List<String> searchPorts()
        {
            String[] ports = SerialPort.GetPortNames();
            return ports.ToList();
        } 

        //Запускает порт
        private Boolean makeActive()
        {
            try
            {
                port.Handshake = Handshake.None;
                port.Open();
                port.DtrEnable = true;
                connectionActive = true;
                return true;
            }
            catch (InvalidOperationException) { return false; }
        }

        //Закрывает порт
        private void closeConnection()
        {
            try
            {
                port.RtsEnable = false;
                port.DtrEnable = false;
                port.DiscardInBuffer();
                port.DiscardOutBuffer();
                port.Close();
                connectionActive = false;
            }
            catch (InvalidOperationException) { }
            
        }

        //Преобразует четность
        private Parity parityConvert(int parity)
        {
            if (parity == (int)_Parity.Even) return Parity.Even;    //Число единиц всегда д.б. четным
            if (parity == (int)_Parity.Mark) return Parity.Mark;    //Бит четности всегда = 1
            if (parity == (int)_Parity.None) return Parity.None;    //Нет контроля четности
            if (parity == (int)_Parity.Odd) return Parity.Odd;      //Нечетное число единиц
            if (parity == (int)_Parity.Space) return Parity.Space;  //Бит четности = 0
            return Parity.None;
        }

        //Преобразование стоп-битов из числа в спец. тип
        private StopBits stopBitsConvert(double stopBits)
        {
            if (stopBits == 0) return StopBits.One;
            if (stopBits == 1) return StopBits.One;
            if (stopBits == 2) return StopBits.Two;
            if (stopBits == 1.5) return StopBits.OnePointFive;
            return StopBits.One;
        }

        //Проверяет наличие запроса DTR второго компьютера
        public Boolean testConnection()
        {
            try{
                if (port.DsrHolding)
                    return true;
                return false;
            }
            catch(InvalidOperationException){
                return false;
            }
        }

        //Проверяет готовность второго компьютера к приему
        public Boolean receiverReady()
        {
            try
            {
                port.RtsEnable = true;
                if (port.CtsHolding && testConnection())
                    return true;
                return false;
            }
            catch (InvalidOperationException) { return false; }
        }

        //Пересылает 1 байт данных
        public Boolean sendByte(byte _byte)
        {
            if (receiverReady())
            {
                byte[] buf = new byte[1];
                buf[0] = _byte;
                try
                {
                    port.Write(buf, 0, 1);
                }
                catch(InvalidOperationException)
                {
                    return false;
                }
                
                return true;
            }
            return false;
            
        }

        //Пересылает кадр. Возвращает количество переданных байт кадра
        public int sendFrame(byte[] frame)
        {
            int sended = 0;
            foreach (byte _byte in frame)
            {
                if (sendByte(_byte))
                    sended++;
                else return sended;
            }
            return sended;
        }

        //Прием данных версия 2
        public void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (port.BytesToRead > port.ReadBufferSize)
                    port.RtsEnable = false;
                byte[] buffer = new byte[port.BytesToRead];
                port.Read(buffer, 0, buffer.Length);
                if (port.BytesToRead < port.ReadBufferSize)
                    port.RtsEnable = true;
                for (int i = 0; i < buffer.Length; i++)
                {
                    Console.WriteLine(buffer[i]);
                }
            }
            catch (TimeoutException) { return; }
        }

//Ненужный код (не надо удалять)-------------------------------------------------------------------------------

        //Пересылает кадр. Возвращает количество переданных байт кадра
        public int sendFrameV2(byte[] frame)
        {
            int sended = 0;
            int attempts = 0;
            for (int i = 0; i < frame.Length; i++)
            {
                if (sendByte(frame[i]))
                {
                    attempts = 0;
                }
                else
                {
                    attempts++;
                    if (attempts > 5)
                        return i;
                    i--;
                }
            }
            return -1;
        }


        //Прием данных старая функция
        public void DataReceivedOLD(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //Обработка принятого байта
                int _byte = port.ReadByte();
                Console.WriteLine(_byte);
            }
            catch (TimeoutException) { return; }
            if (port.BytesToRead != 0)
            {
                while (port.BytesToRead != 0)
                {
                    Console.WriteLine(port.ReadByte());
                }
            }
        }

        //Поток приема
        public void readThread()
        {
            while (connectionActive)
            {
                try
                {
                    if (port.BytesToRead > 1)
                        port.RtsEnable = false;
                    byte receivedByte = (byte)port.ReadByte();
                    if (port.BytesToRead == 0)
                        port.RtsEnable = true;
                    received++;
                    Console.WriteLine(received);
                }
                catch (TimeoutException) { }
            }
        }
    }
}
