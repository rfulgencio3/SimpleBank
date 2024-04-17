using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Core.Domains.Entities;

public class Card : Base
{
    public int AccountId { get; set; }
    public string DisplayName { get; set; }
    public Status Status { get; set; }
    public CardType CardType { get; set; }
    public long CardNumber { get; set; }
    public string Expiration { get; set; }
    public string Last4 { get; set; }
    public virtual Account Account { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; }
    public Card FromCreateCard(CreateCard card, int accountId, long cardNumber)
    {
        return new Card
        {
            AccountId = accountId,
            Status = Status.Active,
            DisplayName = card.DisplayName,
            CardType = card.CardType,
            CardNumber = cardNumber,
            Expiration = GenerateExpiration(card.CardType),
            Last4 = string.Concat(cardNumber.ToString().TakeLast(4)),
            CreatedAt = DateTime.UtcNow
        };
    }

    public Card FromUpdateCard(UpdateCard card)
    {
        return new()
        {
            Status = card.Status,
            UpdatedAt = DateTime.UtcNow
        };
    }
    public string GenerateExpiration(CardType cardType)
    {
        var expiration = DateTime.UtcNow;

        switch (cardType)
        {
            case CardType.Simple:
                expiration = expiration.AddMonths(18);
                break;
            case CardType.Silver:
                expiration = expiration.AddMonths(24);
                break;
            case CardType.Gold:
                expiration = expiration.AddMonths(36);
                break;
            default:
                expiration = expiration.AddMonths(6);
                break;
        }
        return expiration.ToString("MM/yyyy");
    }
}
