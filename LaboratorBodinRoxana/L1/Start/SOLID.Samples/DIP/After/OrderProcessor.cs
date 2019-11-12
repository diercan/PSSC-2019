using System;
using System.Linq;

namespace SOLID.Samples.Tests.DIP.Before
{
	public class OrderProcessor
	{
		private ITaxStrategy TaxStrategy{get; set;}
		public OrderProcessor(ITaxStrategy taxstrategy)
		{
			TaxStrategy=taxstrategy;
		}

		public void ProcessOrder(Order order)
		{
			order.Total = order.OrderLines.Sum(ol => ol.Amount);

			order.Tax += TaxStrategy.GetTax();

			order.GrandTotal = order.Total + order.Tax;
		}
	public interface ITaxStrategy
	{
		decimal GetTax();
	}
	public enum Country
	{
		UnitedStates,
		UnitedKingdom
	}
	}
}