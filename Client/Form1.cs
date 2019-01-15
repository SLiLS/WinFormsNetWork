using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.Mail;

namespace Client
{
    public partial class Form1 : Form
    {
        static string userName;
        private const string host = "127.0.0.1";
        private const int port = 8888;
        static TcpClient client;
        static NetworkStream stream;
        private static Thread receiveThread;
        public delegate void del();
        string mes;
        ICollection<Credit> Credits;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null || textBox3.Text != null)
            {
                string Name = textBox1.Text.ToString();
                string SecName = textBox2.Text.ToString();
                string Mail = textBox3.Text.ToString();


                

                try
                {
                    
                    client = new TcpClient();

                    client.Connect(host, port);
                    stream = client.GetStream();
                  
                   
                   
                   
                    mes ="Имя"+","+ Name + "," + SecName + "," + Mail;
                    userName = mes;
                    SendMessage(mes);
                    receiveThread = new Thread(ReceiveMessage);
                    receiveThread.Start();


                  
                    textBox1.Visible = false;
                    button1.Visible = false;
                    textBox2.Visible = false;
                    textBox3.Visible = false;
                    textBox4.Visible = true;
                  
                    comboBox1.Visible = true;
                    textBox6.Visible = true;
                    label1.Visible = false;
                    label2.Visible = false;
                    label3.Visible = false;
                    tabControl1.Visible = true;
                    label4.Visible = true;
                  
                    label6.Visible = true;
                    label7.Visible = true;
                    button4.Visible = true;
                    button2.Visible = true;
                    richTextBox1.Visible = true;
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                  
                    SendMessage("Credits");
                }




            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }
        public void ReceiveMessage()
        {
            while (true)
            {


                byte[] data = new byte[64]; // буфер для получаемых данных 
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                do
                {
                    bytes = stream.Read(data, 0, data.Length);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (stream.DataAvailable);

                string message = builder.ToString();
                if (message.Contains("Credits"))
                {

                    message = message.Substring(7);
                    string[] names = message.Split(',');

                    Invoke(new MethodInvoker(() =>
                    {

                        comboBox1.Items.AddRange(names);
                        comboBox1.SelectedItem = names[0];

                    }));
                    message = null;
                }
                else
                {
                    if (message.Contains("Рассчёт"))
                    {

                        Invoke(new MethodInvoker(() =>
                        {
                        richTextBox1.Text = null;
                        richTextBox1.Text += "Название кредита" + "      " + "Процент" + "     " + "Минимальная прибыль"; ;

                        string[] split = message.Split(',');
                        for (int j = 0; j < split.Length; j++)
                        {
                            if (j % 5 != 0)
                            {
                                richTextBox1.Text += split[j];
                                richTextBox1.Text += " ";
                            }
                            else
                            {
                                richTextBox1.Text += "\n";
                            }
                        }
                        comboBox1.Items.Clear();
                            for (int i = 0; i < split.Length; i++)
                            {

                                  if (split[i].Contains("года"))
                                    {
                                        comboBox1.Items.Add(split[i]);

                                    }

                                

                            }


                            comboBox1.SelectedItem = split[1];
                           //for (int i = 0; i < split.Length/5; i++)
                           //{

                           //}
                       }));
                        message = null;
                    }
                    else
                    {
                        string[] names = message.Split(',');

                    }
                }

            }

        }
        static void Disconnect()
        {
            if (stream != null)
                stream.Close();//отключение потока
            if (client != null)
                client.Close();//отключение клиента
            Environment.Exit(0); //завершение процесса
        }
        static void SendMessage(string message)
        {

            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("rafalovich99@yandex.ru");
            mail.Subject = "Анализ рентабельности проекта";
            mail.Body = "Сохранённые кредиты"+"\n"+richTextBox2.Text;
            mail.To.Add(new MailAddress(userName.Split(',')[3]));
            SmtpClient client = new SmtpClient("smtp.yandex.ru");
            client.Port = 25;
            client.Credentials = new NetworkCredential("rafalovich99","230799" );
            client.EnableSsl = true;
            client.Send(mail);
            MessageBox.Show("Сообщение отправлено", "Отправка", MessageBoxButtons.OK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string mess = "Рассчёт" + "," + textBox4.Text+ ","+textBox6.Text;
            SendMessage(mess);
            mess = null;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            string sele ="Сохрани"+","+ comboBox1.SelectedItem.ToString()+","+userName.Split(',')[3];
            SendMessage(sele);
            richTextBox2.Text += comboBox1.SelectedItem.ToString();
            richTextBox2.Text+="\n";
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
