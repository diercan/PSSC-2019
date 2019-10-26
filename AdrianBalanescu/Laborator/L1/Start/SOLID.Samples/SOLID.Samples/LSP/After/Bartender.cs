namespace SOLID.Samples.Tests.LSP.After
{
	public class Bartender : Adult
	{
		public bool CanPersonDrinkAlcohol(Person person)
		{
			bool isAdult = false;

			if (person is Adult)
				isAdult = true;

			return isAdult;
		}
	}
}