using SimpleBank.Application.Services.Interfaces;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;
using SimpleBank.Core.Repositories;

namespace SimpleBank.Application.Services;
public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public async Task<Account> GetAccountByIdAsync(long id)
    {
        return await _repository.GetAccountByIdAsync(id);
    }

    public async Task<Account> GetAccountByNumberAsync(int accountNumber)
    {
        return await _repository.GetAccountByNumberAsync(accountNumber);
    }
    public async Task<IEnumerable<Account?>> GetAllAsync()
    {
        return await _repository.GetAllAccountsAsync();
    }

    public async Task<Account> CreateAccountAsync(CreateAccount createAccount)
    {
        try
        {
            var account = new Account().FromCreateAccount(createAccount);
            account.AccountNumber = _repository.GetNextAccountNumberAsync().Result;

            return await _repository.CreateAccountAsync(account);
        }
        catch (Exception ex)
        {
            throw new Exception($"ERROR_TO_CREATE_ACCOUNT: {ex.Message.ToString()}");
        }
    }

    public async Task<Account?> UpdateAccountAsync(int accountNumber, UpdateAccount accountDTO)
    {
        try
        {
            var account = new Account().FromUpdateAccount(accountDTO);
            var findAccount = await _repository.GetAccountByNumberAsync(accountNumber);
            if (findAccount is null)
                return findAccount;

            findAccount.Status = account.Status;
            findAccount.Email = account.Email;

            return await _repository.UpdateAccountAsync(findAccount);
        }
        catch (Exception ex)
        {
            throw new Exception($"ERROR_TO_UPDATE_ACCOUNT: {ex.Message.ToString()}");
        }
        
    }

    public async Task<bool> DeleteAccountAsync(int accountNumber)
    {
        try
        {
            var findAccount = await _repository.GetAccountByNumberAsync(accountNumber);
            if (findAccount is null)
                return false;

            return await _repository.DeleteAccountAsync(findAccount.Id);
        }
        catch (Exception ex)
        {
            throw new Exception($"ERROR_TO_DELETE_ACCOUNT: {ex.Message.ToString()}");
        }
    }
}
