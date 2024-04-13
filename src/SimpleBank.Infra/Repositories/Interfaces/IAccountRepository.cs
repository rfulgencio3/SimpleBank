using SimpleBank.Core.Domains.Entities;

namespace SimpleBank.Infra.Repositories.Interfaces;

public interface IAccountRepository 
{
    Task<Account> GetAccountByIdAsync(long id);
    Task<Account> GetAccountByNumberAsync(int accountNumber);
    Task<IEnumerable<Account?>> GetAllAccountsAsync();
    Task<Account> CreateAccountAsync(Account account);
    Task<Account> UpdateAccountAsync(Account account);
    Task<bool> DeleteAccountAsync(long id);
    Task<int> GetNextAccountNumberAsync();
}
