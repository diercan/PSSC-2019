using System;

namespace SOLID.Samples.SRP.Before
{
	public class Person
	{
		private const decimal _minimumRequiredBalance = 10m;

		public Person(string name, decimal accountBalance)
		{
			Name = name;
			Balance = accountBalance;
		}

		public string Name { get; private set; }

		public decimal Balance { get; private set; }

		public decimal AvailableFunds
		{
			get { return Balance - _minimumRequiredBalance; }
		}

		public void DeductFromBalanceBy(decimal amountToDeduct)
		{
			if (amountToDeduct > Balance)
				throw new InvalidOperationException("Cannot deduct more than is available from Account");

			Balance -= amountToDeduct;
		}
	}
}
