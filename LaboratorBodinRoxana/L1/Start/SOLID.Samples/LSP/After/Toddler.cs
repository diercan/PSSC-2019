namespace SOLID.Samples.Tests.LSP.Before
{
	public class Toddler : Person
	{
		
		public override bool CanDrinkAlcohol
		{
			get{return false;}
		}
		public override bool ShouldDrinkMilk
		{
			get{return true;}
		}
	}
}