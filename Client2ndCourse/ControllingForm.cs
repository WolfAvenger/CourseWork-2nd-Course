﻿using Newtonsoft.Json;
using PCStatusLib;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client2ndCourse
{
    public partial class ControllingForm : Form
    {
        string host;
        string user_ip;
        int port = 8888;
        static TcpClient client;
        static NetworkStream stream;
        ConnectingForm caller;
        string current_ip = "";
        RichTextBox[] boxes;


        public ControllingForm(string host, string my_ip, ConnectingForm caller)
        {
            InitializeComponent();
            this.host = host;
            user_ip = my_ip;
            this.caller = caller;
            boxes = new RichTextBox[] { processes_richTextBox, disks_richTextBox, virtmem_richTextBox,
                                        physmem_richTextBox, sys_richTextBox };
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
                        Thread.Sleep(40);
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

                        if (comps_listBox.InvokeRequired)
                        {
                            comps_listBox.Invoke(new MethodInvoker(delegate
                            {
                                comps_listBox.Items.Remove(received.Substring(received.IndexOf(':') + 1, received.LastIndexOf(':') - received.IndexOf(':') - 1));
                                comps_listBox.Update();
                            }));
                            continue;
                        }
                        else
                        {
                            comps_listBox.Items.Remove(received.Substring(received.IndexOf(':') + 1, received.LastIndexOf(':') - received.IndexOf(':') - 1));
                            comps_listBox.Update();
                        }
                        continue;
                    }

                    CollectedInfo info;
                    info = JsonConvert.DeserializeObject<CollectedInfo>(received);
                    if (info.need_alert) ShowAlert(info.ip);

                    if (current_ip.CompareTo("") == 0) continue;

                    if (!received.Contains(current_ip)) continue;

                    Edit_Boxes(info);

                }
                catch (JsonException ex) { }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);              //соединение было прервано
                                                                                       //Debug.ReadLine();
                                                                                       //Disconnect();
                    Environment.Exit(0);
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

        void ShowAlert(string ip)
        {
            MessageBox.Show("PC with IP " + ip + " has deviation with usage of virtual or physical memory.");
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
                foreach(RichTextBox textBox in boxes)
                {
                    if (textBox.InvokeRequired)
                    {
                        comps_listBox.Invoke(new MethodInvoker(delegate
                        {
                            textBox.Text = string.Empty;
                        }));
                        continue;
                    }
                    else
                    {
                        textBox.Text = string.Empty;
                    }
                }
            }
        }
    }
}
