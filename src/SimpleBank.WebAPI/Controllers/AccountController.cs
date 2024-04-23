using Microsoft.AspNetCore.Mvc;
using SimpleBank.Application.Services.Interfaces;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace SimpleBank.WebAPI.Controllers;

[ApiController]
[Route("api/v1/account")]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    public AccountController(
        IAccountService accountService
        )
    {
        _accountService = accountService;
    }

    [HttpGet("id/{id}")]
    public async Task<IActionResult> GetAccountById(long id)
    {
        var result = await _accountService.GetAccountByIdAsync(id);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet("{accountNumber}")]
    public async Task<IActionResult> GetAccountByNumber(int accountNumber)
    {
        var result = await _accountService.GetAccountByNumberAsync(accountNumber);
        return result is not null ? Ok(result) : NotFound();
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _accountService.GetAllAsync();
        return result.Count() > 0 ? Ok(result) : NotFound(Enumerable.Empty<Account>());
    }

    [HttpPost("create")]
    public async Task<IActionResult> Post(CreateAccount createAccount)
    {
        var result = await _accountService.CreateAccountAsync(createAccount);
        return result is not null ? Accepted("ACCOUNT_CREATED_WITH_SUCCESS", $"ACCOUNT_NUMBER: {result.AccountNumber}") : BadRequest();
    }

    [HttpPut("update")]
    public async Task<IActionResult> Put(
        [Required][FromHeader] int accountNumber,
        UpdateAccount account)
    {
        var result = await _accountService.UpdateAccountAsync(accountNumber, account);
        return result is not null ? Accepted("ACCOUNT_UPDATED_WITH_SUCCESS", $"ACCOUNT_NUMBER: {result.AccountNumber}") : NotFound();
    }

    [HttpDelete("{accountNumber}")]
    public async Task<IActionResult> Delete(int accountNumber)
    {
        var result = await _accountService.DeleteAccountAsync(accountNumber);
        return result is not false ? Accepted("ACCOUNT_DELETED_WITH_SUCCESS", $"ACCOUNT_NUMBER: {accountNumber}") : NotFound();
    }
}
