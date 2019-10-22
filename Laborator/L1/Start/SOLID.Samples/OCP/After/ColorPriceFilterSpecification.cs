using System.Collections.Generic;
using System.Linq;

namespace SOLID.Samples.Tests.OCP.Before
{
	public class ColorPriceFilerSpecification:FilerSpecification
	{
        public ColorPriceFilterSpecification (Color color,decimal price)
        {
            ColorFilterSpecification=new ColorFilterSpecification(color);
            ColorPriceFilterSpecification=new ColorPriceFilerSpecification(price);

        }
        public ColorFilterSpecification ColorFilterSpecification{get;set;}
        public ColorPriceFilerSpecification ColorPriceFilerSpecification{get;set;}
        public override IEnumerable<Product> Filter(IEnumerable<Product> products)
        {
          var matchedColorProducts=ColorFilterSpecification.Filter(products);
          var matchedColorPriceProducts=ColorPriceFilerSpecification.Filter(products);
          List<Product> matchedProducts=new List<Product>(matchedColorProducts.Concat(matchedColorPriceProducts).Distinct());
          return matchedColorProducts;
        }
    }

}