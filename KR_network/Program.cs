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
        static void Main()
        {

            /*PhysicalLayer pl = new PhysicalLayer("COM2", 1, 1, 8, 1);
            DLL dll = new DLL(pl);
            while (!pl.receiverReady()) { }
            Console.WriteLine("sending 'abc'...");
            dll.sendMessage("asdas");
            
            Console.WriteLine("sending 'def'...");
            dll.sendMessage("def");
            Console.ReadKey();
            */
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainMenu()); 
            

        }
        
    }
}
