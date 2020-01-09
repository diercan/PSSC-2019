using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Teambuilding.Models
{
    public class Event
    {

		public Guid id { get; set; }

		[DataType(DataType.Date)]
		public DateTime date { get; set; }

		public String text { get; set; }


	}

}
