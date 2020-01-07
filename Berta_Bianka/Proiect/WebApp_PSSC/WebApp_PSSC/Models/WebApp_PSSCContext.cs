using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApp_PSSC.Models
{
	public class WebApp_PSSCContext:DbContext
	{
		public WebApp_PSSCContext() : base("WebApp_PSSCContext")
		{

		}

		public DbSet<Client> Clienti { get; set; }
		public DbSet<Produs> Produse { get; set; }
		public DbSet<Comanda> Comenzi { get; set; }
	}
}