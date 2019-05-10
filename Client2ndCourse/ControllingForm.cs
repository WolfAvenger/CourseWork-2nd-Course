using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
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
        string current_ip = "";


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

                    if (received.Contains("on a server"))
                    {
                        comps_listBox.Items.Add(received.Substring(received.IndexOf(':') + 1, received.LastIndexOf(':') - received.IndexOf(':')-1));
                        comps_listBox.Update();
                        continue;
                    }
                    else if (received.Contains("left the server"))
                    {
                        comps_listBox.Items.Remove(received.Substring(received.IndexOf(':') + 1, received.LastIndexOf(':') - received.IndexOf(':')-1));
                        comps_listBox.Update();
                        continue;
                    }

                    if (current_ip.CompareTo("") == 0) continue;

                    if (!received.Contains(current_ip)) continue;
                    using (FileStream s = new FileStream("get_info.txt", FileMode.Create))
                    {
                        StreamWriter w = new StreamWriter(s);
                        w.WriteLine(received);
                        w.Close();
                    }
                    CollectedInfo info;
                    using (FileStream s = new FileStream("get_info.txt", FileMode.Open))
                    {
                        DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(CollectedInfo));
                        info = (CollectedInfo)js.ReadObject(s);
                    }

                    Edit_Boxes(info);

                }
                catch (SerializationException ex) { }
                catch (IOException ex) { }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);              //соединение было прервано
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

        void Edit_Boxes(CollectedInfo info)
        {
            string elems = "";
            foreach (string elem in info.processes) elems += elem + "\r\n";
            processes_richTextBox.Text = elems;

            virtmem_richTextBox.Text = info.av_virt_mem.ToString();
            physmem_richTextBox.Text = info.av_phys_mem.ToString();

            proc_richTextBox.Text = info.cpu;

            elems = "";
            foreach (DiskInfo elem in info.disks_info)
            {
                elems += elem.GetFields();
                elems += Environment.NewLine + "\r\n";
            }
            disks_richTextBox.Text = elems;

            sys_richTextBox.Text = info.sys_info.GetFields();
        }

        private void comps_listBox_SelectedValueChanged(object sender, EventArgs e)
        {
            current_ip = comps_listBox.SelectedValue.ToString();
        }

        private void ControllingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Disconnect();
        }

        private void comps_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                current_ip = comps_listBox.Items[comps_listBox.SelectedIndex].ToString();
                ip_label.Text = current_ip;
            }
            catch(Exception ex)
            {
                current_ip = "";
                ip_label.Text = current_ip;
            }
        }
    }
}
