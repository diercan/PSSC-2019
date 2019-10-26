namespace SOLID.Samples.Tests.LSP.Before
{
	public class Mother : Adult
	{
		public bool ShouldGiveBottleOfMilkTo(Person person)
		{
			bool isToddler = false;

			if (person is Toddler)
				isToddler = true;
			
			return isToddler;
		}
	}
}