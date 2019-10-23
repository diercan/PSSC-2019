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

		public IEnumerable<Product> FilterByPrice(decimal price)
		{
			foreach (var product in Products)
				if (product.Price == price)
					yield return product;
		}

		public IEnumerable<Product> FilterByColor(Color color)
		{
			foreach (var product in Products)
				if (product.Color == color)
					yield return product;
		}

		public IEnumerable<Product> FilterBySizeAndPrice(Color color, decimal price)
		{
			foreach (var product in Products)
				if (product.Color == color && product.Price == price)
					yield return product;
		}
	}
}