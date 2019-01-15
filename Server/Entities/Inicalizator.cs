using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Models;
using System.Data.Entity;

namespace Server.Entities
{
    class Inicalizator : DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context db)
        {
            db.Credits.Add(new Models.CreditInformation { Name = "Потребительский на 1 года", Procent = 15, Time = 1, Currency = "Бел. рубль", MinProfit = 1000 });
            db.Credits.Add(new Models.CreditInformation { Name = "Потребительский на 2 года", Procent = 14, Time = 2, Currency = "Бел. рубль", MinProfit = 2000 });
            db.Credits.Add(new Models.CreditInformation { Name = "Потребительский на 3 года", Procent = 15, Time = 3, Currency = "Бел. рубль", MinProfit = 2500 });
            db.Credits.Add(new Models.CreditInformation { Name = "Потребительский на 4 года", Procent = 16, Time = 4, Currency = "Бел. рубль", MinProfit = 3000 });
            db.Credits.Add(new Models.CreditInformation { Name = "Потребительский на 4 года", Procent = 17, Time = 4, Currency = "Бел. рубль", MinProfit = 6000 });
            db.Credits.Add(new Models.CreditInformation { Name = "Потребительский на 4 года", Procent = 18, Time = 4, Currency = "Бел. рубль", MinProfit = 7000 });
            db.SaveChanges();
            db.Clients.Add(new Models.Client { Mail = "rafalovich99@mail.ru", Name = "Рафалович Степан" });
            db.SaveChanges();
        }
    }
}
