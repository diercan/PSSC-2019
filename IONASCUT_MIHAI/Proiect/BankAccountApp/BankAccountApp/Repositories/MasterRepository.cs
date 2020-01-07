namespace BankAccountApp.Repositories
{
    public class MasterRepository
    {
        public static IAccountRepository AccountRepository { get; private set; }

        public static void InstantiateRepository()
        {
            if(AccountRepository == null)
                AccountRepository = new AccountRepository();
        }
    }
}
