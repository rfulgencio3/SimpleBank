using SimpleBank.Core.Domains.Entities;

namespace SimpleBank.Infra.Repositories.Interfaces;

public interface ICardRepository 
{
    Task<Card> GetCardByNumberAsync(long cardNumber);
    Task<IEnumerable<Card?>> GetCardsByAccountNumberAsync(int accountNumber);
    Task<Card> CreateCardAsync(Card card);
    Task<Card> UpdateCardAsync(Card card);
    Task<long> GetNextCardNumberAsync();
}
