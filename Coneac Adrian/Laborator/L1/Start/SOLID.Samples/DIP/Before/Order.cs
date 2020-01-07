using System.Collections.Generic;

namespace SOLID.Samples.Tests.DIP.Before
{
	public class Order
	{
		public Order(params OrderLine[] orderLines)
		{
			OrderLines = orderLines;
		}

		public IEnumerable<OrderLine> OrderLines { get; private set; }
		public decimal Total { get; set; }
		public decimal Tax { get; set; }
		public decimal GrandTotal { get; set; }
	}
}