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

		private ProductFilter Filter ;

		public void FilterProductsByColor()
		{
			var colorFilteredProducts = Filter.FilterBy(new ColorFilterSpecification(ColorFilterSpecification.Blue));

			var priceFilteredProducts = Filter.FilterBy(new ColorPriceFilerSpecification(10));

			var priceAndColorFilteredProducts=Filter.FilterBy(new ColorPriceFilerSpecification(Color.Blue,10));
		}
	}
}
