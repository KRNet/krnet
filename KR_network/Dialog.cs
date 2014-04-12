using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KR_network
{
    public partial class Dialog : Form
    {
        public Dialog()
        {
            InitializeComponent();
            messages.Items.Add("waiting for connection");
            Data.appLayer.setForm(this);
            Data.appLayer.SendManageMessage(Msg.ManageType.REQUEST_CONNECT);
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            if (Data.physicalLayer.receiverReady())
            {
                if (richTextBox1.Text != "")
                {
                    Data.appLayer.SendInfoMessage(richTextBox1.Text);
                    messages.Items.Add(richTextBox1.Text);
                }
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
