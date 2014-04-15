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
/*
            PhysicalLayer pl = new PhysicalLayer("COM1", 1, 1, 8, 1);
            DLL dll = new DLL(pl);
            while (!pl.receiverReady()) { }
            Console.WriteLine("sending 'abc'...");
            dll.sendMessage("1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234A");
            
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
