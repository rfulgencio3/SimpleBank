using SimpleBank.Core.Domains.Entities;

namespace SimpleBank.Core.Repositories;

public interface ICardRepository 
{
    Task<Card> GetCardByNumberAsync(long cardNumber);
    Task<IEnumerable<Card?>> GetCardsByAccountNumberAsync(int accountNumber);
    Task<Card> CreateCardAsync(Card card);
    Task<Card> UpdateCardAsync(Card card);
    Task<long> GetNextCardNumberAsync();
}
