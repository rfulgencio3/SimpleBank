using AutoBogus;
using FluentAssertions;
using Moq;
using SimpleBank.Application.Services;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.ValueObjects;
using SimpleBank.Core.Repositories;
using Gender = SimpleBank.Core.Domains.Enums.Gender;

namespace SimpleBank.Tests.Application.Services;

public class AccountServiceTests
{
    private readonly AccountService _service;
    private readonly Mock<IAccountRepository> _accountRepositoryMock = new(); 

    public AccountServiceTests()
    {
        _service = new AccountService(_accountRepositoryMock.Object);
    }

    [Fact]
    public void GetAccountByIdAsync_Should_ReturnAccountWithSuccess()
    {
        // Arrange
        var account = AutoFaker.Generate<Account>();

        _accountRepositoryMock.Setup(x => x.GetAccountByIdAsync(It.IsAny<long>())).ReturnsAsync(account);

        // Act
        var result = _service.GetAccountByIdAsync(account.Id).Result;

        // Assert
        result.Id.Should().Be(account.Id); //FluentAssertions
        result.Should().NotBeNull();
        _accountRepositoryMock.Verify(x => x.GetAccountByIdAsync(It.IsAny<long>()), Times.Once);
    }

    [Theory]
    [InlineData(Gender.Male)]
    [InlineData(Gender.Female)]
    [InlineData(Gender.Other)]
    [InlineData(Gender.PreferNotToSay)]
    [Trait("GetAccountByNumberAsync", "Gender")]
    public void GetAccountByNumberAsync_Should_ReturnAccountWithSuccess(Gender gender)
    {
        // Arrange
        var account = new AutoFaker<Account>()
            .RuleFor(x => x.Gender, gender)
            .RuleFor(x => x.HolderName, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.BirthDate, f => f.Date.Between(DateTime.Today.AddYears(-70), DateTime.Today.AddYears(-18)))
            .Generate();

        _accountRepositoryMock.Setup(x => x.GetAccountByNumberAsync(It.IsAny<int>())).ReturnsAsync(account);

        // Act
        var result = _service.GetAccountByNumberAsync(account.AccountNumber).Result;

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(account.Id);
        result.Gender.Should().Be(gender);
        result.BirthDate.Should().NotHaveYear(DateTime.Today.AddYears(-71).Year);
        result.BirthDate.Should().NotHaveYear(DateTime.Today.AddYears(-17).Year);
        _accountRepositoryMock.Verify(x => x.GetAccountByIdAsync(It.IsAny<long>()), Times.Never);
        _accountRepositoryMock.Verify(x => x.GetAllAccountsAsync(), Times.Never);
        _accountRepositoryMock.Verify(x => x.GetAccountByNumberAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void GetAllAccounts_Should_ReturnAccountWithSuccess()
    {
        // Arrange
        var accounts = new AutoFaker<Account>()
            .RuleFor(x => x.Gender, f => f.PickRandom<Gender>())
            .RuleFor(x => x.HolderName, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.BirthDate, f => f.Date.Between(DateTime.Today.AddYears(-70), DateTime.Today.AddYears(-18)))
            .Generate(10);

        _accountRepositoryMock.Setup(x => x.GetAllAccountsAsync()).ReturnsAsync(accounts);

        // Act
        var result = _service.GetAllAsync().Result;

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(10);
        _accountRepositoryMock.Verify(x => x.GetAccountByIdAsync(It.IsAny<long>()), Times.Never);
        _accountRepositoryMock.Verify(x => x.GetAccountByNumberAsync(It.IsAny<int>()), Times.Never);
        _accountRepositoryMock.Verify(x => x.GetAllAccountsAsync(), Times.Once);
    }

    [Fact]
    public void CreateAccount_Should_ReturnAccountWithSuccess()
    {
        // Arrange
        var createAccount = AutoFaker.Generate<CreateAccount>();
        
        _accountRepositoryMock.Setup(x => x.CreateAccountAsync(It.IsAny<Account>())).ReturnsAsync(It.IsAny<Account>());

        // Act
        var result = _service.CreateAccountAsync(createAccount);

        // Assert
        result.Should().NotBeNull();
        result.IsCompletedSuccessfully.Should().BeTrue();
        _accountRepositoryMock.Verify(x => x.CreateAccountAsync(It.IsAny<Account>()), Times.Once);
        _accountRepositoryMock.Verify(x => x.GetNextAccountNumberAsync(), Times.Once);
    }

    [Fact]
    public void CreateAccount_Should_ReturnFail()
    {
        // Arrange
        var createAccount = AutoFaker.Generate<CreateAccount>();

        _accountRepositoryMock.Setup(x => x.CreateAccountAsync(It.IsAny<Account>())).Returns(Task.FromResult<Account>(null));

        // Act
        var result = _service.CreateAccountAsync(createAccount);

        // Assert
        result.IsCompletedSuccessfully.Should().BeTrue();
        result.As<Account>().Should().BeNull();
        _accountRepositoryMock.Verify(x => x.CreateAccountAsync(It.IsAny<Account>()), Times.Once);
    }

    [Fact]
    public void UpdateAccount_Should_ReturnAccountWithSuccess()
    {
        // Arrange
        var accountNumber = AutoFaker.Generate<int>();
        var updateAccount = AutoFaker.Generate<UpdateAccount>();
        var account = AutoFaker.Generate<Account>();

        _accountRepositoryMock.Setup(x => x.UpdateAccountAsync(It.IsAny<Account>())).ReturnsAsync(It.IsAny<Account>());
        _accountRepositoryMock.Setup(x => x.GetAccountByNumberAsync(It.IsAny<int>())).ReturnsAsync(account);

        // Act
        var result = _service.UpdateAccountAsync(accountNumber, updateAccount);

        // Assert
        result.Should().NotBeNull();
        result.IsCompletedSuccessfully.Should().BeTrue();
        _accountRepositoryMock.Verify(x => x.CreateAccountAsync(It.IsAny<Account>()), Times.Never);
        _accountRepositoryMock.Verify(x => x.UpdateAccountAsync(It.IsAny<Account>()), Times.Once);
        _accountRepositoryMock.Verify(x => x.GetAccountByNumberAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public void DeleteAccount_Should_ReturnTrue()
    {
        // Arrange
        var account = AutoFaker.Generate<Account>();

        _accountRepositoryMock.Setup(x => x.DeleteAccountAsync(It.IsAny<long>())).ReturnsAsync(true);
        _accountRepositoryMock.Setup(x => x.GetAccountByNumberAsync(It.IsAny<int>())).ReturnsAsync(account);

        // Act
        var result = _service.DeleteAccountAsync(account.AccountNumber);

        // Assert
        result.Should().NotBeNull();
        result.IsCompletedSuccessfully.Should().BeTrue();
        _accountRepositoryMock.Verify(x => x.CreateAccountAsync(It.IsAny<Account>()), Times.Never);
        _accountRepositoryMock.Verify(x => x.UpdateAccountAsync(It.IsAny<Account>()), Times.Never);
        _accountRepositoryMock.Verify(x => x.DeleteAccountAsync(It.IsAny<long>()), Times.AtLeastOnce);
        _accountRepositoryMock.Verify(x => x.GetAccountByNumberAsync(It.IsAny<int>()), Times.Once);
    }
}
