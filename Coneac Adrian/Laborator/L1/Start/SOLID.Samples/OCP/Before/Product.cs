namespace SOLID.Samples.Tests.OCP.Before
{
	public class Product
	{
		public virtual decimal Price { get; private set; }
		public virtual Color Color { get; private set; }
	}
}