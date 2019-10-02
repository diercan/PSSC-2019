using System;

namespace SOLID.Samples.SRP.After
{
	public class Person
	{
		public Account account;
		public Person(string name, decimal accountBalance)
		{
			Name = name;
			Balance = accountBalance;
		}

		public string Name { get; private set; }
	}
}
