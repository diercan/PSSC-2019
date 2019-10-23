using System.Collections.Generic;

namespace SOLID.Samples.Tests.OCP.After
{
	public class ProductFilter
	{
		public ProductFilter(IEnumerable<Product> products)
		{
			Products = products;
		}

		public IEnumerable<Product> Products { get; private set; }
    }

    public class Price : ProductFilter
    {
        public Price(IEnumerable<Product> products) : base(products)
        {
            Products = products;
        }
        public new IEnumerable<Product> Products { get; private set; }

        public IEnumerable<Product> filterbyprice(decimal price)
        {
            foreach (var product in Products)
                if (product.Price == price)
                {
                    yield return product;
                }
        }
    }

    public class Colorf : ProductFilter
    {
        public Colorf(IEnumerable<Product> products) : base(products)
        {
            Products = products;
        }
        public new IEnumerable<Product> Products { get; private set; }

       
        public IEnumerable<Product> filterbycolor(Color color)
        {
            foreach (var product in Products)
                if (product.Color == color)
                    yield return product;
        }

        public class PriceColor : ProductFilter
        {
            public PriceColor(IEnumerable<Product> products) : base(products)
            {
                Products = products;
            }
            public new IEnumerable<Product> Products { get; private set; }

            public IEnumerable<Product> filterbysizeandprice(Color color, decimal price)
            {
                foreach (var product in Products)
                    if (product.Color == color && product.Price == price)
                        yield return product;
            }
        }
    }