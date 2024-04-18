using Microsoft.EntityFrameworkCore;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Repositories;
using SimpleBank.Infra.Models;

namespace SimpleBank.Infra.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly SimpleBankContext _dbContext;

    public AccountRepository(SimpleBankContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Account> GetAccountByIdAsync(long id)
    {
        return await _dbContext.Accounts
            .FirstOrDefaultAsync(a => a.Id == id);
    }
    public async Task<Account> GetAccountByNumberAsync(int accountNumber)
    {
        return await _dbContext.Accounts
            .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
    }
    public async Task<IEnumerable<Account?>> GetAllAccountsAsync()
    {
        return await _dbContext.Accounts.ToListAsync();
    }

    public async Task<Account> CreateAccountAsync(Account account)
    {
        var entry = await _dbContext.Accounts.AddAsync(account);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        _dbContext.Accounts.Update(account);
        await _dbContext.SaveChangesAsync();
        return account;
    }

    public async Task<bool> DeleteAccountAsync(long id)
    {
        var account = await _dbContext.Accounts
            .FirstOrDefaultAsync(a => a.Id == id);

        if (account != null)
        {
            _dbContext.Set<Account>().Remove(account);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<int> GetNextAccountNumberAsync()
    {
        var maxAccountNumber = await _dbContext.Accounts.MaxAsync(a => (int?)a.AccountNumber);
        return maxAccountNumber.HasValue ? maxAccountNumber.Value + 1 : 100;
    }
}
