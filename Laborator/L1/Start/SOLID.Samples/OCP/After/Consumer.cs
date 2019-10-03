using System.Collections.Generic;
using SOLID.Samples.OCP.After;

namespace SOLID.Samples.OCP.After
{
	public class Consumer : IFilterProductsBy
	{
		public Consumer()
		{
			FilterPrice = new ProductFilterPrice(new List<Product>());
		}

		protected ProductFilterPrice FilterPrice { get; set; }

		public void FilterProductsBy()
		{
			var greenFilteredProducts = FilterPrice.Filter(); // this will filter by price

			// wee need a class for size and price
			//var smallFilteredProducts = Filter.FilterSizeAndPrice(Color.Red, 10);
		}
	}
}
