using Newtonsoft.Json;
using PCStatusLib;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client2ndCourse
{
    public partial class UserForm : Form
    {
        string host;
        string user_ip;
        int port = 8888;
        static TcpClient client;
        static NetworkStream stream;
        double E_v = 0, E_p = 0;
        double D_v = 0, D_p = 0;
        static int first_message_counter = 15; //не забудь поменять до 150
        static int message_control_count = 10; //100
        int iteration = 0, iteration_1 = 0;
        bool take = true; bool is_in = false; bool alert = false; bool alert_1 = false;
        double[] phys = new double[first_message_counter], virt = new double[first_message_counter];
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
                MessageBox.Show("You have put server's IP, which is not correct or server is not working now."+
                    " Restart the program and input correct IP address.");
                Environment.Exit(0);
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

                Checking_Alerting(info);

                message = JsonConvert.SerializeObject(info);

                byte[] data = Encoding.Unicode.GetBytes(message);
                Thread.Sleep(40);
                stream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                //Close();
                //sending_info.Interrupt();
                //Environment.Exit(0);
                MessageBox.Show(ex.Message + Environment.NewLine);
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

        void Checking_Alerting(CollectedInfo info)
        {
            if (take && iteration < first_message_counter)
            {
                E_p += info.av_phys_mem;
                E_v += info.av_virt_mem;
                phys[iteration] = info.av_phys_mem;
                virt[iteration] = info.av_virt_mem;
                iteration++;
            }
            else
            {
                if (!is_in)
                {
                    E_p /= first_message_counter;
                    E_v /= first_message_counter;
                    double sum = 0;
                    foreach (double x in phys)
                    {
                        sum += Math.Pow(x - E_p, 2);
                    }
                    sum /= first_message_counter - 1;
                    D_p = sum; sum = 0; //есть физ-дисперсия
                    foreach (double x in virt)
                    {
                        sum += Math.Pow(x - E_v, 2);
                    }
                    sum /= first_message_counter - 1;
                    D_v = sum; sum = 0; //есть вирт-дисперсия
                    take = false; is_in = true;
                    iteration = 0; iteration_1 = 0;
                }
                else
                {

                    bool option = !((info.av_phys_mem > (E_p - 3 * D_p)) && (info.av_phys_mem < (E_p + 3 * D_p)));
                    if (option)
                    {
                        iteration++;
                        if(iteration >= message_control_count)
                            alert = true;
                    }
                    else
                    {
                        iteration = 0;
                        alert = false;
                    }
                    option = (info.av_virt_mem < E_v - 3 * D_v || info.av_virt_mem > E_v + 3 * D_v);
                    if (option)
                    {
                        iteration_1++;
                        if (iteration_1 >= message_control_count)
                            alert_1 = true;
                    }
                    else
                    {
                        iteration_1 = 0;
                        alert_1 = false;
                    }
                    info.need_alert = alert || alert_1;
                    if (alert) { alert = !alert; iteration = 0; }
                    if (alert_1) { alert_1 = !alert_1; iteration_1 = 0; }                
                }
            }
        }

    }
}
