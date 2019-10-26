using SOLID.Samples.Tests.OCP.After;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.Samples.OCP.After.Specs
{
    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }
}
