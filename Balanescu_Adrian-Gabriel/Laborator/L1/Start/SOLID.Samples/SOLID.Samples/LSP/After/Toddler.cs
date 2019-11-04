namespace SOLID.Samples.LSP.After
{
	public class Toddler : Person
	{
		public override bool CanDrinkAlcohol()
		{
			return false;
		}

		public override bool CanDrinkMilk()
		{
			return true;
		}
	}
}