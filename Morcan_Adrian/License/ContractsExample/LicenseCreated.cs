using System;

namespace ContractsExample
{
	public class LicenseCreated
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public int Quantity { get; set; }
	}
}
