using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook_MVC.Models.UserComponents
{
    public class BankAccount
    {
        private IBAN iban;
        private float amount;
        public BankAccount(IBAN iban, float amount)
        {
            this.iban = iban;
            this.amount = amount;
        }

        public IBAN GetIBAN { get; }
        public float GetAmount { get; }
        public void Deposit(float amount)
        {
            this.amount += amount;
        }
        public void Transfer(BankAccount account, float amount)
        {
            if (this.amount - amount > 0)
            {
                account.Deposit(amount);
                this.amount -= amount;
            }
            else throw new Exceptions.InsufficientFundsException("Insufficient funds to make transaction!");
        }
    }
}

