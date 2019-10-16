using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOLID.Samples.SRP.After
{
    class Account
    {
        public Account(decimal Balance)
        {
            this.Balance = Balance;
        }
        private const decimal _minimumRequiredBalance = 10m;

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
