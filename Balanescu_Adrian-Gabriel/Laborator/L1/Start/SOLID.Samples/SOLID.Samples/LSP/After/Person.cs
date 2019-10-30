/*
 * Clasa Person e practic o interfata ce
 * obliga clasele derivate sa aibe acelasi
 * comportament in cazul substituirii unui obiect
 * de tip Person cu un obiect de tip derivat din Person
 */
namespace SOLID.Samples.LSP.After
{
	public abstract class Person
	{
		public abstract bool CanDrinkAlcohol();
		public abstract bool CanDrinkMilk();
	}
}