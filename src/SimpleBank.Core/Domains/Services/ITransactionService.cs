using SimpleBank.Core.Domains.DTOs;
using SimpleBank.Core.Domains.Entities;

namespace SimpleBank.Core.Services;

public interface ITransactionService
{
    Task<IEnumerable<Transaction?>> GetTransactionsByCardNumberAsync(long cardNumber);
    Task<Transaction> CreateTransactionAsync(long cardNumber, CreateTransactionDTO transactionDTO);
}