using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCStatusLib;
using Newtonsoft.Json;

namespace Client2ndCourse
{
    public partial class UserForm : Form
    {
        string host;
        string user_ip;
        int port = 8888;
        static TcpClient client;
        static NetworkStream stream;
        Thread sending_info;
        ConnectingForm caller;

        public UserForm(string host, string user_ip, ConnectingForm caller)
        {
            InitializeComponent();
            this.host = host;
            this.user_ip = user_ip;
            this.caller = caller;
            textBox1.Text += "[IP]: "+host;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            client = new TcpClient();

            try
            {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                string message = "[IP]:" + user_ip;
                byte[] data = Encoding.Unicode.GetBytes(message);
                stream.Write(data, 0, data.Length);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Введенный сервер недоступен или выключен. Перезапустите программу и введите IPv4-адрес доступного сервера. " + ex.Message + " Ошибка подключения к серверу");
                Close();
                caller.Show();
                return;
            }
            this.Show();

            ThreadStart ts1 = new ThreadStart(Background);
            sending_info = new Thread(ts1);
            sending_info.IsBackground = true;
            sending_info.Start();
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            sending_info.Abort();
        }

        public void SendMessage()
        {

            try
            {
                string message;

                CollectedInfo info = PCStatus.CollectAllInfo();
                message = JsonConvert.SerializeObject(info);

                byte[] data = Encoding.Unicode.GetBytes(message);
                Thread.Sleep(40);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                Dispose();
                Close();
                sending_info.Interrupt();
                Environment.Exit(0);
            }
        }

        void Background()
        {
            while (true)
            {
                SendMessage();
                Thread.Sleep(2000);
            }
        }

        private void UserForm_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            sending_info.Abort();
            Environment.Exit(0);
        }
    }
}
