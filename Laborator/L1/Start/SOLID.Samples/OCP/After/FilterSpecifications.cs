using System.Collections.Generic;

namespace SOLID.Samples.Tests.OCP.Before
{
	public abstract class FilerSpecification
	{
        public abstract IEnumerable<Product> Filter(IEnumerable<Product> products);
    }

}