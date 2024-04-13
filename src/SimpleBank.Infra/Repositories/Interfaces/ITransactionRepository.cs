using SimpleBank.Core.Domains.Entities;

namespace SimpleBank.Infra.Repositories.Interfaces;

public interface ITransactionRepository 
{
    Task<IEnumerable<Transaction?>> GetTransactionsByCardNumberAsync(long cardNumber);
    Task<Transaction> CreateTransactionAsync(Transaction transaction);
}
