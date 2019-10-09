using SOLID.Samples.DIP.After;
using System;
using System.Linq;

namespace SOLID.Samples.Tests.DIP.After
{
	
	public class OrderProcessor
	{
        private readonly ITaxCalculator _taxCalculator;
		public OrderProcessor(ITaxCalculator taxCalculator)
		{
            _taxCalculator = taxCalculator;
		}

		public void ProcessOrder(Order order)
		{
			order.Total = order.OrderLines.Sum(ol => ol.Amount);

            order.Tax += _taxCalculator.GetTax();

			order.GrandTotal = order.Total + order.Tax;
		}
	}
}