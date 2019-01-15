using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
   public class CreditInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Procent { get; set; }
        public decimal Time { get; set; }
        public string Currency { get; set; }
        public decimal MinProfit { get; set; }

        public ICollection<Client> Clients { get; set; }
        public CreditInformation()
        {
            Clients = new List<Client>();
        }
    }
}
