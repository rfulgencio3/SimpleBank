using SimpleBank.Core.Domains.Entities;

namespace SimpleBank.Core.Repositories;

public interface ITransactionRepository 
{
    Task<IEnumerable<Transaction?>> GetTransactionsByCardNumberAsync(long cardNumber);
    Task<Transaction> CreateTransactionAsync(Transaction transaction);
}
