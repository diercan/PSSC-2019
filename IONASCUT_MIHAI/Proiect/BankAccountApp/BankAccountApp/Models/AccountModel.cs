namespace BankAccountApp.Models
{
    public class AccountModel
    {
        public string IBAN { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public double Balance { get; private set; }
        public string ExpiryDate { get; private set; }

        public AccountModel()
        {
        }

        public AccountModel(string IBAN, string name, string surname, double balance, string expiryDate)
        {
            this.IBAN = IBAN;
            Name = name;
            Surname = surname;
            Balance = balance;
            ExpiryDate = expiryDate;
        }

        public void setIBAN(string IBAN)
        {
            this.IBAN = IBAN;
        }

        public void setName(string name)
        {
            Name = name;
        }

        public void setSurname(string surname)
        {
            Surname = surname;
        }

        public void setBalance(double balance)
        {
            Balance = balance;
        }

        public void setExpiryDate(string expiryDate)
        {
            ExpiryDate = expiryDate;
        }

        public void addMoney(double sum)
        {
            Balance += sum;
        }

        public void retrieveMoney(double sum)
        {
            Balance -= sum;
        }
    }
}
