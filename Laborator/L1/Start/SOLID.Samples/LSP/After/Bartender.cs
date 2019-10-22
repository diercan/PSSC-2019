namespace SOLID.Samples.Tests.LSP.Before
{
	public class Bartender
	{
		public bool CanPersonDrinkAlcohol(Person person)
		{
			return person.CanDrinkAlcohol;
		}
	}
}