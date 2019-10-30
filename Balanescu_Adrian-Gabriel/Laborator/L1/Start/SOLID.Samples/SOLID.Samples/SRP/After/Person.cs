/*
 * Initial clasa Person avea ca responsabilitati
 * retinerea informatiilor despre o persoana si
 * efectuarea operatiilor asupra contului aferent
 * Acum responsabilitatiile sunt impartite intre
 * clasa Person si clasa Account
 */
namespace SOLID.Samples.SRP.After
{
	public class Person
	{
        private Account _personAccount;
		public Person(string name, decimal accountBalance)
		{
			Name = name;
            _personAccount = new Account(accountBalance);
		}

		public string Name { get; private set; }

	}
}
