using System.Collections.Generic;

namespace SOLID.Samples.OCP.After
{
	public class ProductFilterPrice : IFilter<Product>
	{
		private decimal m_price;
		public ProductFilterPrice(IEnumerable<Product> products)
		{
			Products = products;
			Price = m_price;
		}
		public decimal Price{get; set;}

		public IEnumerable<Product> Products { get; private set; }

		public IEnumerable<Product> Filter()
		{
			foreach (var product in Products)
				if (product.Price == this.Price)
					yield return product;
		}

		/*Two more classes needed for the other two filter */
		// public IEnumerable<Product> FilterColor(Color color)
		// {
		// 	foreach (var product in Products)
		// 		if (product.Color == color)
		// 			yield return product;
		// }

		// public IEnumerable<Product> FilterSizeAndPrice(Color color, decimal price)
		// {
		// 	foreach (var product in Products)
		// 		if (product.Color == color && product.Price == price)
		// 			yield return product;
		// }
	}
}