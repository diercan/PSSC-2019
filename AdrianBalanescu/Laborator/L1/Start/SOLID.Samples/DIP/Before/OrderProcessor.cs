using System;
using System.Linq;

namespace SOLID.Samples.Tests.DIP.Before
{
	public class TaxCalculator
	{
		public decimal GetUSTax()
		{
			return 1m;
		}
	}

	public class OrderProcessor
	{
		public OrderProcessor()
		{
			TaxCalculator = new TaxCalculator();
		}

		protected TaxCalculator TaxCalculator { get; set; }

		public void ProcessOrder(Order order)
		{
			order.Total = order.OrderLines.Sum(ol => ol.Amount);

			order.Tax += TaxCalculator.GetUSTax();

			order.GrandTotal = order.Total + order.Tax;
		}


	}
}