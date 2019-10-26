using System;

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
