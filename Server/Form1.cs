using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Entities;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using Server.Models;
using Server.Helpers;

namespace Server
{
    public partial class Form1 : Form
    {
        Context db = new Context();
        private const int port = 8888;
        private static TcpListener listener;
        public void AddClient(string Name,string SecName, string Email )
        {
            string name = Name + " " + SecName;
            db.Clients.Add(new Client {Name=name,Mail=Email });
            db.SaveChanges();
        }
        public void AddCredit(string Name, decimal time, decimal minprofit, decimal proc, string curren)
        {
            db.Credits.Add(new CreditInformation { Name = Name, Time = time, MinProfit = minprofit, Procent = proc, Currency = curren });
            db.SaveChanges();
        }
       
        public Form1()
        {
         

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread lis = new Thread(Lis);
            lis.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.Clients.Add(new Client { Name = textBox1.Text + " " + textBox2.Text, Mail = textBox3.Text });
            db.SaveChanges();
        }

        private void button2_Click(object sender, EventArgs e)
        {

         

            //try
            //{
            //    listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            //    listener.Start();

            //    while (true)
            //    {
            //        TcpClient client = listener.AcceptTcpClient();
            //        ClientObject clientObject = new ClientObject(client);
            //        Thread clientThread = new Thread(clientObject.Process);
            //        clientThread.Start();
            //    }

            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    if (listener != null)
            //    {
            //        listener.Stop();
            //    }
            //}
            
        }
        public void Lis()
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                listener.Start();

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client);
                    Thread clientThread = new Thread(clientObject.Process);
                    clientThread.Start();
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                }
            }
        }
    }
}
