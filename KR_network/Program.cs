using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
using System.Windows.Forms;

namespace KR_network
{
    class Program
    {
 //       static byte[] frame = new byte[1000];

//        static PhysicalLayer pl = new PhysicalLayer("COM1", 9600, 10, 8, 1);

        static void Main()
        {
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu());
            */
            PhysicalLayer pl = new PhysicalLayer("COM1", 9600, 1, 8, 1);
            DLL dll = new DLL(pl);
            while (!pl.receiverReady()) { }
            dll.sendMessage("msg abc def msg abc def msg abc def msg abc def msg abc def");
            Console.Read();
            Console.Read();
            
        }
        
        /*static public void write()
        {
            
            while (true){
               if ( pl.sendFrame(frame) == 1000)
                Console.WriteLine("Sended");
                Thread.Sleep(500);
            }
                
        }*/
        //static void Main(string[] args)
        //{
            /*
            String[] portList = SerialPort.GetPortNames();
            SerialPort port = new SerialPort( "COM1" , 9600, Parity.None, 8, StopBits.One);
            port.Open();
            Console.WriteLine(port.IsOpen);
            String a;
            a = Console.ReadLine();
            Console.WriteLine(a);
            byte[] data = {1};
            port.Write(data, 0, 1);
            port.Write(a);
            port.Close();
            Console.ReadLine();
            */
            //PhysicalLayer pl = new PhysicalLayer("COM1");
            //Console.WriteLine( PhysicalLayer.isActivePort("COM1"));

            /*
            Console.WriteLine("COM1");
            SerialPort p1 = new SerialPort("COM1");
            p1.Handshake = Handshake.None;
            p1.Open();
            p1.DtrEnable = true;
            Console.Write("Получен сигнал DTR? ");
            Console.WriteLine(p1.DsrHolding);
            while (true)
            {
                if (p1.DsrHolding)
                {
                    Console.WriteLine(p1.DsrHolding);
                    break;
                }
            }
           // Console.WriteLine(p1.DsrHolding);
            p1.RtsEnable = true;
            Console.Write("Получен сигнал RTS? ");
            Console.WriteLine(p1.CtsHolding);
            while (true)
            {
                if (p1.CtsHolding)
                {
                    Console.WriteLine(p1.CtsHolding);
                    break;
                }
            }
            p1.RtsEnable = false;
            Console.Write("Получена отмена сигнала RTS? ");
            Console.WriteLine(p1.CtsHolding);
            Console.WriteLine(p1.DsrHolding);

            //p1.Close();*/


          //  PhysicalLayer pl = new PhysicalLayer("COM1", 9600, 10, 8, 1);
           // while (!pl.testConnection()){}
           // Console.WriteLine("Connection OK");
            //while (!pl.receiverReady()) { }
            //Console.WriteLine("Receiver OK");
            //byte[] forSend = new byte[50000];
            //int i;
            //byte[] forSend = new byte[5000];
            //for (i = 0; i < forSend.Length; i++ )
            //{
             //   forSend[i] = 5;
                //Console.WriteLine("Sended: " + i.ToString() +" " + pl.sendFrame(forSend).ToString());
           // }
            //byte a = 10;
            //pl.sendByte(a);
            //pl.sendByte(a);
            //Console.WriteLine(pl.receiverReady());
             //   Console.WriteLine("Sended: " + pl.sendFrame(forSend).ToString());
                //Console.WriteLine("Sended: " + pl.sendFrame(forSend).ToString());
                //Console.WriteLine("Sended: " + pl.sendFrame(forSend).ToString());
            //Console.ReadKey();
        //}
    }
}
