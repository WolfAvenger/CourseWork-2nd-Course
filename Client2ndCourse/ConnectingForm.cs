using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Server;
using Client;
using System.Net;

namespace Client2ndCourse
{
    public partial class ConnectingForm : Form
    {
        ControllingForm active_form = new ControllingForm();

        public ConnectingForm()
        {
            InitializeComponent();
        }

        

        private void start_button_Click(object sender, EventArgs e)
        {
            if (admin_checkBox.Checked)
            {
                active_form.Show();
                this.Hide();
            }
            else
            {
                IPAddress IP;
                if (!IPAddress.TryParse(ip_textBox.Text, out IP))
                {
                    throw new ArgumentException();
                }
                string host = ip_textBox.Text;
                string my_ip = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
                UserForm user_form = new UserForm(host, my_ip, this);
                user_form.Show();
                this.Hide();
            }

        }
    }
}
