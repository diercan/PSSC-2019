namespace SOLID.Samples.OCP.After
{
	public class Product
	{
		public virtual decimal Price { get; private set; }
		public virtual Color Color { get; private set; }
	}
}