using SOLID.Samples.OCP.After;
using System.Collections.Generic;

namespace SOLID.Samples.Tests.OCP.After
{
	public class ProductFilter : IFilter<Product>
	{
		public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var item in items)
            {
                if (spec.IsSatisfied(item))
                {
                    yield return item;
                }
            }
        }
	}
}