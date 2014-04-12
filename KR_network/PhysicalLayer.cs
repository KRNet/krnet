using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
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
        public enum _Parity { Even, Mark, None, Odd, Space };
        public Boolean connectionActive = false;
        public SerialPort port;
        public int received = 0;
        private ConcurrentQueue<byte> dataForDLL;

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
                dataForDLL = new ConcurrentQueue<byte>();
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
            List<String>portsList = new List<string>();
            foreach (var port in ports){
                SerialPort p = new SerialPort(port);
                if (!p.IsOpen)
                {
                    portsList.Add(port);
                }
            }
            return portsList;
        }

        //Запускает порт
        private Boolean makeActive()
        {
            try
            {
                port.Handshake = Handshake.None;
                port.Open();
                port.DtrEnable = true;
                connectionActive = false;
                return true;
            }
            catch (InvalidOperationException) { return false; }
            catch (IOException) { return false; }
        }

        //Закрывает порт
        public void closeConnection()
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
            try
            {
                if (port.DsrHolding)
                    return true;
                return false;
            }
            catch (InvalidOperationException)
            {
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
                {
                    this.connectionActive = true;
                    return true;
                }
                    
                return false;
            }
            catch (InvalidOperationException) { return false; }
        }

        //Пересылает 1 байт данных
        private Boolean sendByte(byte _byte)
        {
            if (receiverReady())
            {
                byte[] buf = new byte[1];
                buf[0] = _byte;
                try
                {
                    port.Write(buf, 0, 1);
                }
                catch (InvalidOperationException)
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

        //Метод для канального уровня
        public byte[] getAllFromDllBuffer()
        {
            lock (dataForDLL)
            {
                byte[] buf = new byte[dataForDLL.Count];
                dataForDLL.CopyTo(buf, 0);
                dataForDLL = new ConcurrentQueue<byte>();
                return buf;
            }
        }

        //Откладываем в буфер на прочтение
        private void sendToDLL(byte[] array)
        {
            if (connectionActive)
            {
                lock (dataForDLL)
                {
                    foreach (byte bytik in array)
                        dataForDLL.Enqueue(bytik);
                }
            }
        }

        //Прием данных версия 2
        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (port.BytesToRead >= port.ReadBufferSize)
                    port.RtsEnable = false;
                byte[] buffer = new byte[port.BytesToRead];
                port.Read(buffer, 0, buffer.Length);
                if (port.BytesToRead < port.ReadBufferSize)
                    port.RtsEnable = true;
                sendToDLL(buffer);
            }
            catch (TimeoutException) { return; }
        }


      
    }
}
