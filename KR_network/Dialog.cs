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
        private MainMenu parent;
        public Dialog(MainMenu parent)
        {
            InitializeComponent();
            this.parent = parent;
            messages.Items.Add("waiting for connection");
            Data.appLayer.setForm(this);
            sendBtn.Enabled = false;
            richTextBox1.Enabled = false;
        }

        public MainMenu getParent()
        {
            return parent;
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            if (Data.physicalLayer.receiverReady())
            {
                if (richTextBox1.Text != "")
                {
                    Data.appLayer.SendInfoMessage(richTextBox1.Text);
                    messages.Items.Add(richTextBox1.Text);
                    richTextBox1.Text = "";
                }
            }
            else
            {
                info_text.Text = "Соединение не установлено";
            }
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            if (Data.physicalLayer.receiverReady())
            {
                messages.Items.Add("Пытаюсь закрыть соединение");
                Data.appLayer.SendManageMessage(Msg.ManageType.REQUEST_DISCONNECT);
            }
            else
            {
                this.Close();
                parent.Show();
            }

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                sendBtn_Click(sender, e);
        }
    }
}
