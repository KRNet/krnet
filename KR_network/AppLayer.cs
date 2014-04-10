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
        private ListBox messages;

        public AppLayer(PhysicalLayer physicalLayer, ListBox messages)
        {
            this.physicalLayer = physicalLayer;
            this.dll = new DLL();
            this.messages = messages;
        }

        public void SendInfoMessage(string msg)
        {
            Msg message = new Msg(msg, Msg.Types.info);
            messageQueue.Enqueue(message);
            messages.Items.Add(message.getMessage());
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
                    messages.Items.Add(msg.getMessage());
                }
            }
        }

    }
}
