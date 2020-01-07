using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SosdeanLucian.Models
{
    public class GymContext :DbContext
    {
        public GymContext(): base("GymContext")
        {

        }
        public virtual DbSet<Oferta> Oferte { get; set; }
        public virtual DbSet<Client> Clienti { get; set; }

        public virtual DbSet<Abonament> Abonamente { get; set; }
    }
}