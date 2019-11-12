namespace SOLID.Samples.ISP.Before
{
	public class Dog : IAnimal,IAmGroomable
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