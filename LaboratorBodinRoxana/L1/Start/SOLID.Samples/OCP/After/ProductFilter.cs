using System.Collections.Generic;

namespace SOLID.Samples.Tests.OCP.Before
{
	public class ProductFilter
	{
		public ProductFilter(IEnumerable<Product> products)
		{
			Products = products;
		}

		public IEnumerable<Product> Products { get; private set; }

		public IEnumerable<Product> FilterBy(FilterSpecification filterspecification)
		{
			return filterspecification.Filter(Products);
		}

	}
}