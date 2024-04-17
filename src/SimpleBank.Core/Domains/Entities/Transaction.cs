using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Core.Domains.Entities;

public class Transaction : Base
{
    public Guid TransactionId { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public Card Card { get; set; }

    public Transaction FromCreateTransaction(long cardNumber, CreateTransaction transaction)
    {
        return new Transaction
        {
            TransactionId = Guid.NewGuid(),
            TransactionType = transaction.TransactionType,
            Card = new Card { CardNumber = cardNumber },
            Amount = transaction.Amount,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
