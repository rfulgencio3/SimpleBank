using Microsoft.AspNetCore.Mvc;
using SimpleBank.Core.Domains.ValueObjects;
using SimpleBank.Core.Services;

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
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost("{cardNumber}")]
    public async Task<IActionResult> Post(long cardNumber, CreateTransaction createTransaction)
    {
        var result = await _transactionService.CreateTransactionAsync(cardNumber, createTransaction);
        return result is not null ? Accepted("TRANSACTION_CREATED_WITH_SUCCESS", $"TRANSACTION_ID: {result.Id}") : BadRequest();
    }
}