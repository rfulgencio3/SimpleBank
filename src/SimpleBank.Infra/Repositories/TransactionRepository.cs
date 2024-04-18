using Microsoft.EntityFrameworkCore;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Repositories;
using SimpleBank.Infra.Models;

namespace SimpleBank.Infra.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly SimpleBankContext _dbContext;

    public TransactionRepository(SimpleBankContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Transaction?>> GetTransactionsByCardNumberAsync(long cardNumber)
    {
        return await _dbContext.Transactions
          .Where(c => c.Card.CardNumber == cardNumber)
          .ToListAsync();
    }
    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        var entry = await _dbContext.Transactions.AddAsync(transaction);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }
}
