using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Application.Services.Interfaces;

public interface ICardService
{
    Task<Card> GetCardByNumberAsync(long cardNumber);
    Task<IEnumerable<Card?>> GetCardsByAccountAsync(int accountNumber);
    Task<Card> CreateCardAsync(int accountNumber, CreateCard createCard);
    Task<Card> UpdateCardAsync(int accountNumber, long cardNumber, UpdateCard UpdateAccount);
}