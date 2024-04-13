using SimpleBank.Core.Domains.DTOs;
using SimpleBank.Core.Domains.Entities;

namespace SimpleBank.Core.Services;

public interface IAccountService
{
    Task<Account> GetAccountByIdAsync(long id);
    Task<Account> GetAccountByNumberAsync(int accountNumber);
    Task<IEnumerable<Account?>> GetAllAsync();
    Task<Account> CreateAccountAsync(CreateAccountDTO createAccountDTO);
    Task<Account> UpdateAccountAsync(int accountNumber, UpdateAccountDTO updjateAccountDTO);
    Task<bool> DeleteAccountAsync(int accountNumber);
}