using System;
using System.Collections.Generic;
using System.Text;

namespace ContractsExample
{
	public class LicenseDeleted
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public int Quantity { get; set; }
	}
}
