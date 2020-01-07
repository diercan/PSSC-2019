namespace SOLID.Samples.LSP.After
{
	public class Adult : Person
	{
		public override bool CanDrinkAlcohol()
		{
			return true;
		}

		public override bool CanDrinkMilk()
		{
			return false;
		}
	}
}