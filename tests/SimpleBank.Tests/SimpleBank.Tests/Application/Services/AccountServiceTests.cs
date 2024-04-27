using AutoBogus;
using FluentAssertions;
using Moq;
using SimpleBank.Application.Services;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace SimpleBank.Tests.SimpleBank.Application.Tests.Services;

public class AccountServiceTests
{
    private readonly AccountService _service;
    private readonly Mock<IAccountRepository> _repositoryMock = new();

    public AccountServiceTests()
    {
        _service = new AccountService(_repositoryMock.Object);        
    }

    [Fact]
    [Trait("GetAccount", "GetAccountById")]
    public async void GetAccountById_Should_ReturnAccount()
    {
        // Arrange
        var account = new AutoFaker<Account>()
            .RuleFor(x => x.Status, Status.Active)
            .RuleFor(x => x.HolderName, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .Generate();

        _repositoryMock.Setup(x => x.GetAccountByIdAsync(account.Id)).ReturnsAsync(account);

        // Act
        var result = _service.GetAccountByIdAsync(account.Id).Result;

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(account.Id);
        result.Status.Should().Be(Status.Active);
        _repositoryMock.Verify(x => x.GetAccountByIdAsync(account.Id), Times.Once);
        _repositoryMock.Verify(x => x.UpdateAccountAsync(It.IsAny<Account>()), Times.Never);
    }

    public async void GetAccountByNumber_Should_ReturnAccount()
    {
        // Arrange
        var account = new AutoFaker<Account>()
            .RuleFor(x => x.Status, Status.Active)
            .RuleFor(x => x.HolderName, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .Generate();

        _repositoryMock.Setup(x => x.GetAccountByNumberAsync(It.IsAny<int>())).ReturnsAsync(account);

        // Act
        var result = _service.GetAccountByNumberAsync(account.Id).Result;

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(account.Id);
        result.Status.Should().Be(Status.Active);
        _repositoryMock.Verify(x => x.GetAccountByNumberAsync(account.Id), Times.Once);
        _repositoryMock.Verify(x => x.UpdateAccountAsync(It.IsAny<Account>()), Times.Never);
    }

    [Fact]
    public void DeleteAccount_Should_ReturnTrue()
    {
        // Arrange
        var account = AutoFaker.Generate<Account>();

        _repositoryMock.Setup(x => x.GetAccountByNumberAsync(It.IsAny<int>())).ReturnsAsync(account);
        _repositoryMock.Setup(x => x.DeleteAccountAsync(It.IsAny<long>())).ReturnsAsync(true);

        // Act
        var result = _service.DeleteAccountAsync(account.AccountNumber);

        // Assert
        result.Should().NotBeNull();
        result.IsCompletedSuccessfully.Should().BeTrue();
        result.Result.Equals(true);
        _repositoryMock.Verify(x => x.GetAccountByNumberAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(x => x.DeleteAccountAsync(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public void DeleteAccount_Should_ReturnFalse()
    {
        // Arrange
        var account = AutoFaker.Generate<Account>();

        _repositoryMock.Setup(x => x.GetAccountByNumberAsync(It.IsAny<int>())).Returns(Task.FromResult<Account>(null));

        // Act
        var result = _service.DeleteAccountAsync(account.AccountNumber);

        // Assert
        result.Should().NotBeNull();
        result.IsCompletedSuccessfully.Should().BeTrue();
        result.Result.Equals(false);
        _repositoryMock.Verify(x => x.GetAccountByNumberAsync(It.IsAny<int>()), Times.Once);
        _repositoryMock.Verify(x => x.DeleteAccountAsync(It.IsAny<long>()), Times.Never);
    }

    [Fact(Skip ="Ajuste de método para validação")]
    [Trait("GetAccount", "GetAllAcounts")]
    public void GetAll_Should_Return_AccountList()
    {
        // Arrange
        var accounts = AutoFaker.Generate<Account>(10);

        _repositoryMock.Setup(x => x.GetAllAccountsAsync()).ReturnsAsync(accounts);

        // Act
        var result = _service.GetAllAsync();

        // Assert
        result.Result.Should().NotBeNull();
        result.IsCompletedSuccessfully.Should().BeTrue();
        result.Result.Count().Should().Be(10);
        _repositoryMock.Verify(x => x.GetAllAccountsAsync(), Times.Once);
    }

    //Task<IEnumerable<Account?>> GetAllAsync();
    //Task<Account> CreateAccountAsync(CreateAccount createAccount);
    //Task<Account> UpdateAccountAsync(int accountNumber, UpdateAccount updateAccount);
}
