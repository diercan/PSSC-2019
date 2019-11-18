using System;

namespace SOLID.Samples.SRP.Before
{
	public class Person
	{
		private string name;
		Account account;

		public Person(string name, Account account)
		{
			Name = name;
			this.account.Balance = account.Balance;
		}

		public string Name { get; private set; }

		public decimal Balance { get; private set; }

	}
}
