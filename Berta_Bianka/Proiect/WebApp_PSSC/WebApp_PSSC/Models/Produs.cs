using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp_PSSC.Models
{
	public class Produs
	{
		[Key]
		public int IdProdus { get; set; }
		public string TipProdus { get; set; }
		public string NumeProdus { get; set; }
		public int Stoc { get; set; }
	}
}