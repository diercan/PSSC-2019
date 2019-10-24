namespace SOLID.Samples.Tests.LSP.After
{
	public class Bartender
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