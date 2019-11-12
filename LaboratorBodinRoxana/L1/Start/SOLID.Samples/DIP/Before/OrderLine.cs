namespace SOLID.Samples.Tests.DIP.Before
{
	public class OrderLine
	{
		public OrderLine(string title, decimal amount)
		{
			Title = title;
			Amount = amount;
		}

		private string Title { get; set; }
		public decimal Amount { get; set; }
	}
}