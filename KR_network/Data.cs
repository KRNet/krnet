using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KR_network
{
    static class Data
    {
        static public int DLLSendTimeout = 10;
        static public int DLLSendRepeatTime = 200;
        static public int DLLReadFromPLTimeout = 100;

        static public byte INFOFrame = 1;
        static public byte RETFrame = 2;
        static public byte ACKFrame = 3;

        static public byte STOPByte = 254;
        static public byte STARTByte = 253;


        static public AppLayer appLayer = null;
        static public DLL dll = null;
        static public PhysicalLayer physicalLayer = null;

        static public void makeAppLayer()
        {
            appLayer = new AppLayer(physicalLayer);   
        }
        
        static public void makePhysicalLayer(string _portName, int _speed, int _parity, int what, double _stopBits)
        {
            physicalLayer = new PhysicalLayer(_portName, _speed, _parity, what, _stopBits);
        }

        static public void makeDLL()
        {
            dll = new DLL(physicalLayer);
        }
    }
}
