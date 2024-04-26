using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleBank.Application.Services.Interfaces;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;
using SimpleBank.WebAPI.Controllers;

namespace SimpleBank.Tests.Controllers;

public class AccountControllerTests
{
    private readonly AccountController _controller;
    private readonly Mock<IAccountService> _serviceMock = new();

    public AccountControllerTests()
    {
        _controller = new AccountController(_serviceMock.Object);
    }

    [Fact]
    public async Task GetAccountById_Should_ReturnsOkResult()
    {
        // Arrange
        var id = 1;

        _serviceMock.Setup(x => x.GetAccountByIdAsync(It.IsAny<long>())).ReturnsAsync(It.IsAny<Account>());

        // Act
        var result = await _controller.GetAccountById(id);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Account>(result);
    }

    [Fact]
    public async Task GetAccountByNumber_Should_ReturnsOkResult()
    {
        // Arrange
        int accountNumber = 123;

        // Act
        var result = await _controller.GetAccountByNumber(accountNumber) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Account>(result.Value);
    }

    [Fact]
    public async Task GetAllAccounts_Should_ReturnsOkResult()
    {
        // Act
        var result = await _controller.GetAll() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<Account>>(result.Value);
        Assert.NotEmpty((IEnumerable<Account>)result.Value);
    }

    [Fact]
    public async Task PostAccount_Should_ReturnsAcceptedResult()
    {
        // Arrange
        var identificationNumber = "123";
        var holderName = "Legolas";
        var birthDate = new DateTime(1990, 11, 20);
        var gender = Gender.Male;
        var email = "legolas@lotr.com";

        var createAccount = new CreateAccount(identificationNumber, holderName, birthDate, gender, email);

        _serviceMock.Setup(s => s.CreateAccountAsync(createAccount))
            .ReturnsAsync(new Account().FromCreateAccount(createAccount));

        // Act
        var result = await _controller.Post(createAccount) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(202, result.StatusCode);
    }

    [Fact(Skip = "Not ready yet!")]
    public async Task PutAccount_Should_ReturnsAcceptedResult()
    {
        // Arrange
        int accountNumber = 123;
        var status = Status.Canceled;
        var email = "emailtest@test.com.br";

        var account = new UpdateAccount(status, email);

        // Act
        var result = await _controller.Put(accountNumber, account) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(202, result.StatusCode);
    }

    [Fact]
    public async Task DeleteAccount_Should_ReturnsAcceptedResult()
    {
        // Arrange
        int accountNumber = 123;

        // Act
        var result = await _controller.Delete(accountNumber) as ObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(202, result.StatusCode);
    }
}
