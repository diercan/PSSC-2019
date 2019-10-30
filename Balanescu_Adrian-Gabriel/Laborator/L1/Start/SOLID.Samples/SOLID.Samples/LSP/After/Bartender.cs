namespace SOLID.Samples.LSP.After
{
	public class Bartender : Adult
	{
		public bool CanPersonDrinkAlcohol(Person person)
		{
			return person.CanDrinkAlcohol();
		}
	}
}