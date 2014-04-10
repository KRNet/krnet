using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Windows.Forms;

namespace KR_network
{
    class AppLayer
    {
        private ConcurrentQueue<Msg> messageQueue;
        private DLL dll;
        private PhysicalLayer physicalLayer;

        public AppLayer(PhysicalLayer physicalLayer)
        {
            this.messageQueue = new ConcurrentQueue<Msg>();
            this.physicalLayer = physicalLayer;
            this.dll = new DLL(physicalLayer);
        }

        public void SendInfoMessage(string msg)
        {
            Msg message = new Msg(msg, Msg.Types.info);
            messageQueue.Enqueue(message);
        }

        public void ReadFromDLL()
        {
            while (true)
            {
                //string message = dll.readFromDLLBuffer();
                string message = "";
                if (!message.Equals(""))
                {
                    Msg msg = Msg.toMsg(message);
                }
            }
        }

    }
}
