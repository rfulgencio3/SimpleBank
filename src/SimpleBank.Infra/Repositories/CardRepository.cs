using Microsoft.EntityFrameworkCore;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Infra.Models;
using SimpleBank.Infra.Repositories.Interfaces;

namespace SimpleBank.Infra.Repositories;

public class CardRepository : ICardRepository
{
    private readonly SimpleBankContext _dbContext;

    public CardRepository(SimpleBankContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Card> GetCardByNumberAsync(long cardNumber)
    {
        return await _dbContext.Cards
           .FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
    }

    public async Task<IEnumerable<Card?>> GetCardsByAccountNumberAsync(int accountNumber)
    {
        return await _dbContext.Cards
          .Where(c => c.Account.AccountNumber == accountNumber)
          .ToListAsync();
    }
    public async Task<Card> CreateCardAsync(Card card)
    {
        var entry = await _dbContext.Cards.AddAsync(card);
        await _dbContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<Card> UpdateCardAsync(Card card)
    {
        _dbContext.Cards.Update(card);
        await _dbContext.SaveChangesAsync();
        return card;
    }
    public async Task<long> GetNextCardNumberAsync()
    {
        long startRange = 3521718200000000;
        long endRange = 3521718299999999;

        var maxCardNumber = await _dbContext.Cards.MaxAsync(a => (long?)a.CardNumber);
        long nextCardNumber = maxCardNumber.HasValue ? maxCardNumber.Value + 1 : startRange;

        return maxCardNumber <= endRange ? maxCardNumber.Value : startRange;
    }

}
