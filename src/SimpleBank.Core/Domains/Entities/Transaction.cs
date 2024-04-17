using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Core.Domains.Entities;

public class Transaction : Base
{
    public Guid TransactionId { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public int CardId { get; set; }
    public virtual Card Card { get; set; }

    public Transaction FromCreateTransaction(int cardId, CreateTransaction transaction)
    {
        return new Transaction
        {
            TransactionId = Guid.NewGuid(),
            CardId = cardId,
            TransactionType = transaction.TransactionType,
            Amount = transaction.Amount,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
