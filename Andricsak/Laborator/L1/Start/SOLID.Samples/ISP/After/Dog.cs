namespace SOLID.Samples.ISP.After
{
	public class Dog : IAnimal
	{
		public string Feed()
		{
			return "dog fed";
		}

		public string Groom()
		{
			return "dog groomed";
		}
	}
}