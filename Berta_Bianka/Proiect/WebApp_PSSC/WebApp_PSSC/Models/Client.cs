using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp_PSSC.Models
{
	public class Client
	{
		[Key]
		public int IdClient { get; set; }
		public string Nume { get; set; }
		public string Localitate { get; set; }
		public string Email { get; set; }

		public virtual ICollection<Comanda> Comenzi { get; set; }
	}
}