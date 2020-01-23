using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SkinShopCSGO.Models
{
    public class SkinShopCSGOContext : DbContext
    {
        public SkinShopCSGOContext() : base("SkinShopCSGOContext")
        {
        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Skin> Skins { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }

    }
}