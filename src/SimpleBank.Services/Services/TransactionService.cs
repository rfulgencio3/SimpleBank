using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;
using SimpleBank.Core.Services;
using SimpleBank.Infra.Repositories.Interfaces;

namespace SimpleBank.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICardRepository _cardRepository;

    public TransactionService(
        ITransactionRepository transactionRepository, ICardRepository cardRepository)
    {
        _transactionRepository = transactionRepository;
        _cardRepository = cardRepository;
    }

    public async Task<IEnumerable<Transaction?>> GetTransactionsByCardNumberAsync(long cardNumber)
    {
        return await _transactionRepository.GetTransactionsByCardNumberAsync(cardNumber);
    }

    public async Task<Transaction> CreateTransactionAsync(long cardNumber, CreateTransaction createTransaction)
    {
        try
        {
            var card = await _cardRepository.GetCardByNumberAsync(cardNumber);

            if (card == null || card.CardNumber == 0)
                throw new Exception("CARD_NOT_FOUND");

            var transaction = new Transaction().FromCreateTransaction(cardNumber, createTransaction);

            return await _transactionRepository.CreateTransactionAsync(transaction);
        }
        catch (Exception ex)
        {
            throw new Exception($"ERROR_TO_CREATE_TRANSACTION: {ex.Message.ToString()}");
        }
    }
}
