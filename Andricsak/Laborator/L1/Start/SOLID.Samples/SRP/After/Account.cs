using System;

namespace SOLID.Samples.SRP.Before
{
	public class Account
	{
		private const decimal _minimumRequiredBalance = 10m;
        private decimal balance;
        public decimal Balance{get;set;}
		public void DeductFromBalanceBy(decimal amountToDeduct)
		{
			if (amountToDeduct > Balance)
				throw new InvalidOperationException("Cannot deduct more than is available from Account");

			Balance -= amountToDeduct;
		}

        public decimal AvailableFunds
		{
			get { return Balance - _minimumRequiredBalance; }
		}
	}
}
