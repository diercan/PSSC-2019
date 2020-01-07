using System;
using System.Collections.Generic;

namespace SOLID.Samples.OCP.After
{    
    public interface IFilter<T>
    {
        public IEnumerable<T> Filter();
    }
}