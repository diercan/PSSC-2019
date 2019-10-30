namespace SOLID.Samples.LSP.After
{
	public class Mother : Adult
	{
		public bool ShouldGiveBottleOfMilkTo(Person person)
		{
			return person.CanDrinkMilk();
		}
	}
}