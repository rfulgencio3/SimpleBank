using Moq;
using SimpleBank.Application.Services;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Repositories;

namespace SimpleBank.Tests.Application.Services;

public class AccountServiceTests
{
    private readonly AccountService _service; //Instância do objeto AccountService para realizar as operações
    private readonly Mock<IAccountRepository> _accountRepositoryMock = new(); //Mock das dependencias do objeto AccountService

    public AccountServiceTests()
    {
        _service = new AccountService(_accountRepositoryMock.Object); //Injeção de dependência na instância de AccountService para utilização dos métodos
    }

    [Fact]
    public void GetAccountByIdAsync_Should_ReturnAccountWithSuccess()
    {
        // Arrange
        var id = 1234567890;
        var account = new Account
        {
            Id = id,
            AccountNumber = 1,
            Balance = 0,
            BirthDate = new DateTime(2000, 05, 01),
            Gender = Gender.Female,
        };

        _accountRepositoryMock.Setup(x => x.GetAccountByIdAsync(It.IsAny<long>())).ReturnsAsync(account);

        // Act
        var result = _service.GetAccountByIdAsync(id).Result;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(1, result.AccountNumber);
        Assert.Equal(0, result.Balance);
        Assert.Equal(new DateTime(2000, 05, 01), result.BirthDate);
        Assert.Equal(Gender.Female, result.Gender); 
    }

    public void GetAccountByNumberAsync_Should_ReturnAccountWithSuccess()
    {
        // Arrange
        var number = 1234567890;
        var account = new Account
        {
            Id = 1,
            AccountNumber = number,
            Balance = 0,
            BirthDate = new DateTime(2000, 05, 01),
            Gender = Gender.Female,
        };

        _accountRepositoryMock.Setup(x => x.GetAccountByNumberAsync(It.IsAny<int>())).ReturnsAsync(account);

        // Act
        var result = _service.GetAccountByNumberAsync(number).Result;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(number, result.AccountNumber);
        Assert.Equal(0, result.Balance);
        Assert.Equal(new DateTime(2000, 05, 01), result.BirthDate);
        Assert.Equal(Gender.Female, result.Gender);
    }

    public void GetAllAccounts_Should_ReturnAccountWithSuccess()
    {
        // Arrange
        var account1 = new Account();
        var account2 = new Account();

        var accounts = new List<Account>();
        accounts.Add(account1);
        accounts.Add(account2);

        _accountRepositoryMock.Setup(x => x.GetAllAccountsAsync()).ReturnsAsync(accounts);

        // Act
        var result = _service.GetAllAsync().Result;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(result.Count(), 2);
    }

    //Task<IEnumerable<Account?>> GetAllAsync();
    //Task<Account> CreateAccountAsync(CreateAccount createAccount);
    //Task<Account> UpdateAccountAsync(int accountNumber, UpdateAccount updateAccount);
    //Task<bool> DeleteAccountAsync(int accountNumber);
}
