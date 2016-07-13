using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DnsLookup
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnResolve_Click(object sender, EventArgs e)
        {
            try
            {
                IPHostEntry iphost = Dns.GetHostEntry(txtBoxHostName.Text);
                foreach(IPAddress ip in iphost.AddressList)
                {
                    string ipaddress = ip.AddressFamily.ToString();
                    listBoxIPs.Items.Add(ipaddress);
                    listBoxIPs.Items.Add("--" + ip.ToString());
                }
                txtBoxHostName.Text = iphost.HostName;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to process the request because the following problem occured:\n" + ex.Message, "Exception occured");
            }
        }
    }
}
