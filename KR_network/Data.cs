using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KR_network
{
    static class Data
    {
        static public AppLayer appLayer;
        static public DLL dll;
        static public PhysicalLayer physicalLayer;

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
