using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Application.Services.Interfaces;

public interface IAccountService
{
    Task<Account> GetAccountByIdAsync(long id);
    Task<Account> GetAccountByNumberAsync(int accountNumber);
    Task<IEnumerable<Account?>> GetAllAsync();
    Task<Account> CreateAccountAsync(CreateAccount createAccount);
    Task<Account> UpdateAccountAsync(int accountNumber, UpdateAccount updateAccount);
    Task<bool> DeleteAccountAsync(int accountNumber);
}