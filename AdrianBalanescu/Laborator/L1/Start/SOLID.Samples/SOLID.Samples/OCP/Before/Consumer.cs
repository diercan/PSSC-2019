using System.Collections.Generic;
using SOLID.Samples.Tests.OCP.Before;

namespace SOLID.Samples.OCP.Before
{
	public class Consumer
	{
		public Consumer()
		{
			Filter = new ProductFilter(new List<Product>());
		}

		protected ProductFilter Filter { get; set; }

		public void FilterProductsByColor()
		{
			var greenFilteredProducts = Filter.FilterByColor(Color.Green);

			var smallFilteredProducts = Filter.FilterBySizeAndPrice(Color.Red, 10);
		}
	}
}
