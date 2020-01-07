using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp_PSSC.Models
{
	public class Comanda
	{
		[Key]
		public int IdComanda { get; set; }
		public int IdClient { get; set; }
		public int IdProdus { get; set; }
		[Range(1, 10), Display(Name = "Numar de produse de acest fel comandate")]
		public int NrProduse { get; set; }

		public virtual Client Client { get; set; }
		public virtual Produs Produs { get; set; }
	}
}