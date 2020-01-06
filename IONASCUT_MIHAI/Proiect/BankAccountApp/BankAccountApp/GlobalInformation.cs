using BankAccountApp.Models;

namespace BankAccountApp
{
    public class GlobalInformation
    {
        public static AccountModel Account { get; private set; }

        public static void setAccount(AccountModel account)
        {
            Account = account;
        }
    }
}
