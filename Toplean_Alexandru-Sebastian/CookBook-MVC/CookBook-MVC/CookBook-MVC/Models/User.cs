using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook_MVC.Models.CommonComponents;
using CookBook_MVC.Models.UserComponents;
using CookBook_MVC.Models.Exceptions;

namespace CookBook_MVC.Models
{
    public class User
    {
        private Text Nume, Prenume;
        private CNP cnp;
        private IBAN iban;
        private List<Recipe> recipes = new List<Recipe>();
        private BankAccount bankAccount; 

        public User(Text Nume, Text Prenume, CNP cnp)
        {
            this.Nume = Nume;
            this.Prenume = Prenume;
            this.cnp = cnp;
           
        }
        public void AddBankAccount(BankAccount bankAccount)
        {
            this.bankAccount = bankAccount;

        }
        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }
        public void RemoveRecipe(Recipe recipe)
        {
            recipes.Remove(recipe);
        }
        public void DepositInBankAccount(float amount)
        {
            if (bankAccount != null)
            {
                bankAccount.Deposit(amount);
            }
            else
                throw new InexistentBankAccountException("There is no Bank Account linked to this account!");
        }
        public void TransferInBankAccount(BankAccount account, float amount)
        {
            if (bankAccount != null)
            {
                bankAccount.Transfer(account, amount);
            }
            else
                throw new InexistentBankAccountException("There is no Bank Account linked to this account!");
        }
        public Text GetUserName { get { return this.Nume; } }
        public Text GetUserSurename { get { return this.Prenume; } }
       

        public BankAccount GetBankAccount { get { return this.bankAccount; } }
        public List<Recipe> GetRecipes { get { return recipes; } }

    }
}
