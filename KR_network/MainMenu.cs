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
    public partial class MainMenu : Form
    {
        string _speed;
        int _parity;
        string _stopBits;
        string _nickname;
        string _portName;
        public MainMenu()
        {
            InitializeComponent();
            
            foreach (var port in PhysicalLayer.searchPorts())
                portName.Items.Add(port);
            
            foreach (var speedEl in new int[] { 9600, 4800, 2400, 1200 })
                speed.Items.Add(speedEl);
            
            string[] p = { "Число единиц всегда д.б. четным", "Бит четности всегда = 1", "Нет контроля четности", "Нечетное число единиц", "Бит четности = 0"};
            
            foreach (var parityEl in p )
                parity.Items.Add(parityEl);
            
            foreach (var stopEl in new double[] {0, 1, 1.5, 2})
                stopBytes.Items.Add(stopEl);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            _nickname = nickname.Text;
            if (_nickname == "" || _speed == null || _portName == null || _stopBits == null)
            {
                MessageBox.Show("Wrong parameters");
            }
            else
            {
                PhysicalLayer physicalLayer = new PhysicalLayer(_portName, int.Parse(_speed), _parity, 8, double.Parse(_stopBits));
                this.Hide();
                MessageBox.Show("Wait");
                Dialog sended = new Dialog();
                AppLayer appLayer = new AppLayer(physicalLayer, (ListBox)sended.Controls.Find("messages", false)[0]);
                while (!physicalLayer.receiverReady());
                sended.Show();
               
                
                //Recieved recievedFrom = new Recieved();
                
            }
        }

        private void portName_SelectedIndexChanged(object sender, EventArgs e)
        {
            _portName = portName.SelectedItem.ToString();
        }

        private void speed_SelectedIndexChanged(object sender, EventArgs e)
        {
            _speed = speed.SelectedItem.ToString();
        }

        private void stopBytes_SelectedIndexChanged(object sender, EventArgs e)
        {
            _stopBits = stopBytes.SelectedItem.ToString();
        }

        private void parity_SelectedIndexChanged(object sender, EventArgs e)
        {
            _parity = parity.SelectedIndex;
        }
    }
}
