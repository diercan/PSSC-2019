namespace SOLID.Samples.ISP.After
{
	public class Dog : IFeedable, IGroomable
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