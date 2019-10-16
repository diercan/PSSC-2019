using SOLID.Samples.Tests.OCP.After;

namespace SOLID.Samples.OCP.After.Specs
{
    public class PriceSpecification : ISpecification<Product>
    {
        private decimal price;

        public PriceSpecification(decimal price)
        {
            this.price = price;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Price == price;
        }
    }
}
