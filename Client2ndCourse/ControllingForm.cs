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
using Newtonsoft.Json;
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
                    byte[] data = new byte[4096]; // буфер для получаемых данных
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    string received = "";
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        received += builder.ToString();
                        builder.Clear();
                        Thread.Sleep(300);
                    }
                    while (stream.DataAvailable);

                    received.Replace(" ", "");
                    received.Replace("\n", "");

                    if (received.Contains("on a server"))
                    {
                        if (comps_listBox.InvokeRequired)
                        {
                            comps_listBox.Invoke(new MethodInvoker(delegate
                            {
                                comps_listBox.Items.Add(received.Substring(received.IndexOf(':') + 1, received.LastIndexOf(':') - received.IndexOf(':') - 1));
                                comps_listBox.Update();
                            }));
                            continue;
                        }
                        else
                        {
                            comps_listBox.Items.Add(received.Substring(received.IndexOf(':') + 1, received.LastIndexOf(':') - received.IndexOf(':') - 1));
                            comps_listBox.Update();
                        }
                        
                    }
                    else if (received.Contains("left the server"))
                    {
                        comps_listBox.Items.Remove(received.Substring(received.IndexOf(':') + 1, received.LastIndexOf(':') - received.IndexOf(':')-1));
                        comps_listBox.Update();
                        continue;
                    }

                    if (current_ip.CompareTo("") == 0) continue;

                    if (!received.Contains(current_ip)) continue;

                    CollectedInfo info;

                    info = JsonConvert.DeserializeObject<CollectedInfo>(received);

                    MessageBox.Show(received);

                    Edit_Boxes(info);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);              //соединение было прервано
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

            Environment.Exit(0);
        }

        void Edit_Boxes(CollectedInfo info)
        {
            string elems = "";
            foreach (string elem in info.processes) elems += elem + "\r\n";

            if (processes_richTextBox.InvokeRequired)
            {
                processes_richTextBox.Invoke(new MethodInvoker(delegate
                {
                    processes_richTextBox.Text = elems;
                }));
            }
            else
            {
                processes_richTextBox.Text = elems;
            }

            if (virtmem_richTextBox.InvokeRequired)
            {
                virtmem_richTextBox.Invoke(new MethodInvoker(delegate
                {
                    virtmem_richTextBox.Text = info.av_virt_mem.ToString();
                }));
            }
            else
            {
                virtmem_richTextBox.Text = info.av_virt_mem.ToString();
            }

            if (physmem_richTextBox.InvokeRequired)
            {
                physmem_richTextBox.Invoke(new MethodInvoker(delegate
                {
                    physmem_richTextBox.Text = info.av_phys_mem.ToString();
                }));
            }
            else
            {
                physmem_richTextBox.Text = info.av_phys_mem.ToString();
            }

            if (proc_richTextBox.InvokeRequired)
            {
                proc_richTextBox.Invoke(new MethodInvoker(delegate
                {
                    proc_richTextBox.Text = info.cpu;
                }));
            }
            else
            {
                proc_richTextBox.Text = info.cpu;
            }

            elems = "";
            foreach (DiskInfo elem in info.disks_info)
            {
                elems += elem.GetFields();
                elems += Environment.NewLine + "\r\n";
            }

            if (disks_richTextBox.InvokeRequired)
            {
                disks_richTextBox.Invoke(new MethodInvoker(delegate
                {
                    disks_richTextBox.Text = elems;
                }));
            }
            else
            {
                disks_richTextBox.Text = elems;
            }

            if (sys_richTextBox.InvokeRequired)
            {
                sys_richTextBox.Invoke(new MethodInvoker(delegate
                {
                    sys_richTextBox.Text = info.sys_info.GetFields();
                }));
            }
            else
            {
                sys_richTextBox.Text = info.sys_info.GetFields();
            }

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
