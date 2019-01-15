using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Server.Models;

namespace Server.Entities
{
   public class Context : DbContext
    {
        static Context()
        {
            Database.SetInitializer<Context>(new Inicalizator());

        }
        public Context()
            : base("CreditInf")
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CreditInformation> Credits { get; set; }

    }
}
