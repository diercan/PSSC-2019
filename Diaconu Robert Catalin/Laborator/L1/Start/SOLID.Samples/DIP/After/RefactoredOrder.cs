using System.Collections.Generic;

namespace SOLID.Samples.Tests.DIP.After
{
	public class RefactoredOrder
	{
		public RefactoredOrder(params RefactoredOrderLine[] orderLines)
		{
			OrderLines = orderLines;
		}

		public IEnumerable<RefactoredOrderLine> OrderLines { get; private set; }
		public decimal Total { get; set; }
		public decimal Tax { get; set; }
		public decimal GrandTotal { get; set; }
	}
}