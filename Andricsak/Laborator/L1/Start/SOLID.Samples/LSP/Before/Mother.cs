namespace SOLID.Samples.Tests.LSP.Before
{
	public class Mother
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