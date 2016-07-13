using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            TcpClient tcpClient = new TcpClient(txtHost.Text, Int32.Parse(txtPort.Text));
            NetworkStream ns = tcpClient.GetStream();
            FileStream fs = File.Open("test.txt", FileMode.Open);
            int data = fs.ReadByte();
            while(data != -1)
            {
                ns.WriteByte((byte)data);
                data = fs.ReadByte();
            }
            fs.Close();
            ns.Close();
            tcpClient.Close();
        }
    }
}
