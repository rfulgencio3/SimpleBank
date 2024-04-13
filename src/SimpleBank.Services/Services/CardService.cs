using SimpleBank.Core.Domains.DTOs;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Services;
using SimpleBank.Infra.Repositories.Interfaces;

namespace SimpleBank.Application.Services;

public class CardService : ICardService
{
    private readonly ICardRepository _cardRepository;
    private readonly IAccountRepository _accountRepository;

    public CardService(
        ICardRepository cardRepository, IAccountRepository accountRepository)
    {
        _cardRepository = cardRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Card> GetCardByNumberAsync(long cardNumber)
    {
        return await _cardRepository.GetCardByNumberAsync(cardNumber);
    }

    public async Task<IEnumerable<Card?>> GetCardsByAccountAsync(int accountNumber)
    {
        return await _cardRepository.GetCardsByAccountNumberAsync(accountNumber);
    }

    public async Task<Card> CreateCardAsync(int accountNumber, CreateCardDTO cardDTO)
    {
        try
        {
            var account = await _accountRepository.GetAccountByNumberAsync(accountNumber);

            if (account == null || account.AccountNumber == 0)
                return null;

            var cardNumber = await _cardRepository.GetNextCardNumberAsync();

            if (cardNumber > 0)
            {
                var card = new Card().FromCreateCard(cardDTO, accountNumber, cardNumber);
                return await _cardRepository.CreateCardAsync(card);
            }
            return null;
        }
        catch (Exception ex)
        {
            throw new Exception($"ERROR_TO_CREATE_CARD: {ex.Message.ToString()}");
        }
    }

    public async Task<Card?> UpdateCardAsync(int accountNumber, long cardNumber, UpdateCardDTO cardDTO)
    {
        try
        {
            var account = await _accountRepository.GetAccountByNumberAsync(accountNumber);

            if (account == null || account.AccountNumber == 0)
                return null;

            var card = new Card().FromUpdateCard(cardDTO);

            var findCard = await _cardRepository.GetCardByNumberAsync(cardNumber);

            if (findCard is null)
                return findCard;

            findCard.Status = cardDTO.Status;

            return await _cardRepository.UpdateCardAsync(findCard);
        }
        catch (Exception ex)
        {
            throw new Exception($"ERROR_TO_UPDATE_CARD: {ex.Message.ToString()}"); ;
        }
    }
}
