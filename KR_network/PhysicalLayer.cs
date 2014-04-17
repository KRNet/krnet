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
        public Boolean makeActive()
        {
            try
            {
                connectionActive = false;
                port.Handshake = Handshake.None;
                port.Open();
                port.DtrEnable = true;
                port.RtsEnable = true;
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
            catch (InvalidOperationException) { Console.WriteLine("fuck you"); }

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

        //Проверяет готовность второго компьютера к приему
        public Boolean receiverReady()
        {
            try
            {
                if (port.CtsHolding && port.DsrHolding)
                {
                    this.connectionActive = true;
                    return true;
                }
                return false;
            }
            catch (InvalidOperationException) { return false; }
        }

        //Пересылает кадр. Возвращает количество переданных байт кадра
        public int sendFrame(byte[] frame)
        {
            //if (readyToSend())
            //{
                port.Write(frame, 0, frame.Length);
                return frame.Length;
            //}
        }

        //Проверяет, свободен ли второй комп
        public bool readyToSend()
        {
            try
            {
                return (port.BytesToWrite == 0 && receiverReady());
            }
            catch (InvalidOperationException e)
            {
                return false;
            }
        }

        //Метод для канального уровня
        public byte[] getAllFromDllBuffer()
        {
            lock (dataForDLL)
            {
                byte[] buf = new byte[dataForDLL.Count];
                dataForDLL.CopyTo(buf, 0);
                dataForDLL = new ConcurrentQueue<byte>();
                setReady();
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
                    setBusy();
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
                    setBusy();
                byte[] buffer = new byte[port.BytesToRead];
                port.Read(buffer, 0, buffer.Length);
                if (port.BytesToRead < port.ReadBufferSize)
                    setReady();
                sendToDLL(buffer);
            }
            catch (TimeoutException) { return; }
        }

        //Устанавливает режим занятости порта
        //Второй комп не может слать кадры
        private void setBusy()
        {
            port.RtsEnable = false;
        }

        //Устанавливает режим готовности к приему
        private void setReady()
        {
            port.RtsEnable = true;
        }
    }
}
