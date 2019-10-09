namespace SOLID.Samples.Tests.OCP.After
{
	public class Product
	{
		public virtual decimal Price { get; private set; }
		public virtual Color Color { get; private set; }
	}
}