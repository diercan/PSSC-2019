/*
 * Interfetele sunt separate astfel incat
 * clasele ce le implementeaza sa nu fi nevoite
 * sa aiba metode neimplimentate.
 */
namespace SOLID.Samples.ISP.After
{
	public class Ant : IFeedable
	{
		public string Feed()
		{
			return "ant fed";
		}
	}
}