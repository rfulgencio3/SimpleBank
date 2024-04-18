using Microsoft.AspNetCore.Mvc;
using SimpleBank.Application.Services.Interfaces;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace SimpleBank.WebAPI.Controllers;

[ApiController]
[Route("api/v1/card")]
public class CardController : Controller
{
    private readonly ICardService _cardService;
    public CardController(
        ICardService cardService
        )
    {
        _cardService = cardService;
    }

    [HttpGet("{cardNumber}")]
    public async Task<IActionResult> GetCardByNumber(long cardNumber)
    {
        var result = await _cardService.GetCardByNumberAsync(cardNumber);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet("account/{accountNumber}")]
    public async Task<IActionResult> GetCardsByAccountNumber(int accountNumber)
    {
        var result = await _cardService.GetCardsByAccountAsync(accountNumber);
        return result.Count() > 0 ? Ok(result) : NotFound(Enumerable.Empty<Card>());
    }

    [HttpPost("create")]
    public async Task<IActionResult> Post(
        [Required][FromHeader] int accountNumber,
        CreateCard createCard)
    {
        var result = await _cardService.CreateCardAsync(accountNumber, createCard);
        return result is not null ? Accepted("CARD_CREATED_WITH_SUCCESS", $"CARD_LAST4_NUMBER: {result.Last4}") : BadRequest();
    }

    [HttpPut("update")]
    public async Task<IActionResult> Put(
        [Required][FromHeader] int accountNumber,
        [Required][FromHeader] long cardNumber,
        UpdateCard updateCard)
    {
        var result = await _cardService.UpdateCardAsync(accountNumber, cardNumber, updateCard);
        return result is not null ? Accepted("CARD_UPDATED_WITH_SUCCESS", $"CARD_LAST4_NUMBER: {result.Last4}") : NotFound();
    }
}
