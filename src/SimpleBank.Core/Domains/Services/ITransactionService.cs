using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Core.Services;

public interface ITransactionService
{
    Task<IEnumerable<Transaction?>> GetTransactionsByCardNumberAsync(long cardNumber);
    Task<Transaction> CreateTransactionAsync(long cardNumber, CreateTransaction transaction);
}