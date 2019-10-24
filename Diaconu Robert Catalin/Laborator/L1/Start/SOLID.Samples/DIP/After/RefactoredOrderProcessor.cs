using System;
using System.Linq;

namespace SOLID.Samples.Tests.DIP.After
{
	public class TaxCalculator
	{
		public decimal GetUSTax()
		{
			return 1m;
		}
	}

	public class RefactoredOrderProcessor
	{
        protected TaxCalculator TaxCalculator { get; set; }

        public RefactoredOrderProcessor(TaxCalculator taxCalculator)
		{
            TaxCalculator = taxCalculator;
		}

		public void ProcessOrder(RefactoredOrder order)
		{
			order.Total = order.OrderLines.Sum(ol => ol.Amount);

			order.Tax += TaxCalculator.GetUSTax();

			order.GrandTotal = order.Total + order.Tax;
		}


	}
}