namespace SOLID.Samples.ISP.Before
{
	public class Ant : IAnimal
	{
		public string Feed()
		{
			return "ant fed";
		}

		public string Groom()
		{
			throw new System.NotImplementedException();
		}
	}
}