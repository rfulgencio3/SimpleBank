using Microsoft.EntityFrameworkCore;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.Enums;
using SimpleBank.Infra.Models;
using SimpleBank.Infra.Repositories;

namespace SimpleBank.Tests.Infra.Repositories;

public class AccountRepositoryTests
{
    private readonly AccountRepository _repository;
    private readonly SimpleBankContext _context;

    public AccountRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<SimpleBankContext>()
            .UseInMemoryDatabase(databaseName: "SimpleBankDbMock")
            .Options;

        _context = new SimpleBankContext(options);
        _repository = new AccountRepository(_context);
    }

    [Fact]
    public async Task GetAccountByIdAsync_Should_ReturnAccountWithSuccess()
    {
        // Arrange
        var id = 1234567890;
        var identificationNumber = Guid.NewGuid().ToString();
        var account = new Account
        {
            Id = id,
            IdentificationNumber = identificationNumber,
            AccountNumber = 1,
            HolderName = "Ada Lovelace",
            Balance = 0,
            BirthDate = new DateTime(2000, 05, 01),
            Gender = Gender.Female,
            Email = "ada@lovelace.com"
        };

        // Adicionar e salvar dados ao banco de dados em memória
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAccountByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(1, result.AccountNumber);
        Assert.Equal(0, result.Balance);
        Assert.Equal(new DateTime(2000, 05, 01), result.BirthDate);
        Assert.Equal(Gender.Female, result.Gender);
    }
}