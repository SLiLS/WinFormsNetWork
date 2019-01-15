using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models
{
   public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public ICollection<CreditInformation> Credits { get; set; }
        public Client()
        {
            Credits = new List<CreditInformation>();
        }
    }
}
