using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Server.Entities;
using System.Windows.Forms;
using Server.Models;
using System.Data.Entity;
using System.Net.Mail;

namespace Server.Helpers
{
  public  class ClientObject
    {
        protected internal NetworkStream Stream { get; private set; }
        TcpClient client;
        public Context db;
        IEnumerable<CreditInformation> fullcredits;


        public ClientObject(TcpClient tcpClient)
        {

            client = tcpClient;
            db = new Context();

        }
        public void Process()
        {

            try
            {
                string message;
                Stream = client.GetStream();
                while (true)
                {
                    try
                    {
                        message = GetMessage();


                        if (message.Contains("Credits"))
                        {

                            string namesOfCredits = "";

                            foreach (var credit in db.Credits)
                            {
                                message += credit.Name + ",";

                            }
                            message = message.Substring(0, message.Length - 1);
                            SendMessage(message);

                        }
                        else
                        if (message.Contains("Имя"))
                        {
                            string[] splitted = message.Split(',');

                          
                                db.Clients.Add(new Client { Name = splitted[1] + " " + splitted[2], Mail = splitted[3] });

                                db.SaveChanges();
                            

                        }
                        else
                        {
                            if (message.Contains("Mail"))
                            {
                                MailMessage mail = new MailMessage();
                                mail.From = new MailAddress("rafalovich99@yandex.ru");
                                mail.Subject = "Анализ рентабельности проекта";
                                mail.Body = "iukuk";
                                mail.To.Add(new MailAddress(""));
                                SmtpClient client = new SmtpClient("smtp.yandex.ru");
                                client.Port = 25;
                                client.Credentials = new NetworkCredential("rafalovich99", "230799");
                                client.EnableSsl = true;
                                client.Send(mail);
                                MessageBox.Show("Сообщение отправлено", "Отправка", MessageBoxButtons.OK);
                            }
                            else
                            {
                                if (message.Contains("Рассчёт"))
                                {
                                    
                                    string[] spl = message.Split(',');
                                   
                                  
                                     fullcredits = db.Credits;
                                
                                  
                                        
                                        fullcredits = fullcredits.Where(p => p.Time == decimal.Parse(spl[1]));
                                        
                                        fullcredits = fullcredits.Where(p => p.MinProfit <= decimal.Parse(spl[2]));
                                    if (fullcredits.Count() == 0)
                                    {
                                        fullcredits = db.Credits;
                                    }
                                   
                                    message = null;
                                  
                                      foreach (CreditInformation item in fullcredits)
                                        {
                                            message += "Рассчёт" + "," + item.Name + "," + item.Procent.ToString().TrimEnd('0')+"%" + "," + item.MinProfit.ToString() ;
                                        }
                                    
                                  
                                   
                                    SendMessage(message);
                                    message = null;
                                }
                                else
                                {
                                    if (message.Contains("Сохрани"))
                                    {

                                        string[] splt = message.Split(',');
                                        if (splt[1] != null && splt[2] != null)
                                        {
                                            using (Context database = new Context())
                                            {
                                                string mailclient = splt[2];
                                                var client = database.Clients.Find(2);
                                                string lol = splt[1];
                                                var cr = database.Credits.FirstOrDefault(h => h.Name == lol);
                                                client.Credits.Add(cr);
                                                database.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                        }

                   
                      
                    }
                    catch
                    {
                        message = String.Format("покинул чат");
                        
                        break;
                    }

                }

            }
            catch (Exception ex)
            {
               
              MessageBox.Show(ex.Message);
            }
          
        }
        private string GetMessage()
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (Stream.DataAvailable);
            return builder.ToString();
        }

        public void SendMessage(string message)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            Stream.Write(data, 0, data.Length);
            
        }
        protected internal void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }
}
