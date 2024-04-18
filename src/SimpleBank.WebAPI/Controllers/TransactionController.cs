using Microsoft.AspNetCore.Mvc;
using SimpleBank.Application.Services.Interfaces;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.WebAPI.Controllers;

[ApiController]
[Route("api/v1/transaction")]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;
    public TransactionController(
        ITransactionService transactionService
        )
    {
        _transactionService = transactionService;
    }

    [HttpGet("{cardNumber}")]
    public async Task<IActionResult> GetTransactionsByCardNumber(long cardNumber)
    {
        var result = await _transactionService.GetTransactionsByCardNumberAsync(cardNumber);
        return result.Count() > 0 ? Ok(result) : NotFound(Enumerable.Empty<Transaction>());
    }

    [HttpPost("{cardNumber}")]
    public async Task<IActionResult> Post(long cardNumber, CreateTransaction createTransaction)
    {
        var result = await _transactionService.CreateTransactionAsync(cardNumber, createTransaction);
        return result is not null ? Accepted("TRANSACTION_CREATED_WITH_SUCCESS", $"TRANSACTION_ID: {result.Id}") : BadRequest();
    }
}