using System.Collections.Generic;
using SOLID.Samples.OCP.After.Specs;
using SOLID.Samples.Tests.OCP.After;

namespace SOLID.Samples.OCP.After
{
	public class Consumer
	{
		public Consumer()
		{
			Filter = new ProductFilter();
		}

		protected ProductFilter Filter { get; set; }

		public void FilterProductsByColor()
		{
            var greenFilteredProducts = Filter.Filter(new List<Product>(), new ColorSpecification(Color.Green));
		}
        public void FilterByColorAndPrice()
        {
            var redPricyFilteredProducts = Filter.Filter(new List<Product>(),
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Red), 
                    new PriceSpecification(10)
                ));
        }
	}
}
