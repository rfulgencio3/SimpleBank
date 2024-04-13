using SimpleBank.Core.Domains.DTOs;
using SimpleBank.Core.Domains.Entities;

namespace SimpleBank.Core.Services;

public interface ICardService
{
    Task<Card> GetCardByNumberAsync(long cardNumber);
    Task<IEnumerable<Card?>> GetCardsByAccountAsync(int accountNumber);
    Task<Card> CreateCardAsync(int accountNumber, CreateCardDTO createCardDTO);
    Task<Card> UpdateCardAsync(int accountNumber, long cardNumber, UpdateCardDTO UpdateAccountDTO);
}