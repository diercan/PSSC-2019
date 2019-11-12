namespace SOLID.Samples.Tests.LSP.Before
{
	public class Adult : Person
	{
		public override bool CanDrinkAlcohol
		{
			get{return true;}
		}
		public override bool ShouldDrinkMilk
		{
			get{return false;}
		}
	}
}