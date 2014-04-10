using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace KR_network
{
    class AppLayer
    {
        private ConcurrentQueue<Msg> messageQueue;
        private DLL dll;
        private PhysicalLayer physicalLayer;

        public AppLayer(PhysicalLayer physicalLayer, string nickname)
        {
            this.physicalLayer = physicalLayer;
            this.dll = new DLL();
        }

        public void SendInfoMessage(string msg)
        {
            messageQueue.Enqueue(new Msg(msg, Msg.Types.info));
        }

        public void ReadFromDLL()
        {
            while (true)
            {
                Msg msg = Msg.toMsg(dll.readFromDLLBuffer());
            }
        }

    }
}
