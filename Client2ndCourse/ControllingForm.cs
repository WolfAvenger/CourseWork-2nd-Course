using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PCStatusLib;

namespace Client2ndCourse
{
    public partial class ControllingForm : Form
    {
        string host;
        string user_ip;
        int port = 8888;
        static TcpClient client;
        static NetworkStream stream;
        Thread getting_info;
        ConnectingForm caller;


        public ControllingForm(string host, string my_ip, ConnectingForm caller)
        {
            InitializeComponent();
            this.host = host;
            user_ip = my_ip;
            this.caller = caller;
        }

        private void ControllingForm_Load(object sender, EventArgs e)
        {
            client = new TcpClient();

            try
            {
                client.Connect(host, port); //подключение клиента
                stream = client.GetStream(); // получаем поток

                // запускаем новый поток для получения данных
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                receiveThread.Start(); //старт потока
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введенный сервер недоступен или выключен. Перезапустите программу и введите IPv4-адрес доступного сервера "+ ex.Message, " Ошибка подключения к серверу");
                Dispose();
                Close();
            }
        }

        public void ReceiveMessage()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);


                    string received = builder.ToString();

                    if (received.Contains("чат")) continue;
                    using (StreamWriter w = new StreamWriter(new FileStream("get_info.txt", FileMode.Create)))
                    {
                        w.WriteLine(received);
                    }
                    CollectedInfo info;
                    using (FileStream s = new FileStream("get_info.txt", FileMode.Open))
                    {
                        DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(CollectedInfo));
                        info = (CollectedInfo)js.ReadObject(s);
                    }

                    richTextBox1.Text = info.av_phys_mem.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + ex.StackTrace); //соединение было прервано
                                                              //Debug.ReadLine();
                    Disconnect();
                }
            }
        }
        void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента

            Dispose();
            Close();
            Environment.Exit(0);
        }
    }
}
