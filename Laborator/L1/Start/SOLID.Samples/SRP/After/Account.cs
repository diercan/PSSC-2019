using System;

namespace SOLID.Samples.SRP.After
{
	public class Account
    {
        private const decimal _minimumRequiredBalance = 10m;
        public decimal Balance { get; private set; }
        public Account(decimal Balance)
        {
            this.Balance=Balance;
        }

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
