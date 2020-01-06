using System.Threading.Tasks;
using BankAccountApp.Entities;

namespace BankAccountApp.Repositories
{
    public interface IAccountRepository
    {
        Task AddAccountTask(AccountEntity account);
        Task<AccountEntity> GetAccountTask(string IBAN);
        Task AddMoneyTask(AccountEntity account, double sum);
        Task RetrieveMoneyTask(AccountEntity account, double sum);
        Task TransferMoneyTask(AccountEntity accountsource, AccountEntity accountdestination, double sum);
    }
}
