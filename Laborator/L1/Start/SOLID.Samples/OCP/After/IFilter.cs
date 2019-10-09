using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.Samples.OCP.After
{
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }
}
