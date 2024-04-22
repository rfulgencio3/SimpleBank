using Moq;
using SimpleBank.Application.Services;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Repositories;

namespace SimpleBank.Tests.Application.Services;

public class CardServiceTests
{
    private readonly CardService _service; //Instância do objeto AccountService para realizar as operações
    private readonly Mock<ICardRepository> _cardRepositoryMock = new(); //Mock das dependencias do objeto AccountService
    private readonly Mock<IAccountRepository> _accountRepositoryMock = new(); //Mock das dependencias do objeto AccountService

    public CardServiceTests()
    {
        _service = new CardService(
            _cardRepositoryMock.Object,
            _accountRepositoryMock.Object); //Injeção de dependência na instância de AccountService para utilização dos métodos
    }

    [Fact]
    public void GetCardByNumberAsync_Should_ReturnCardWithSuccess()
    {
        // Arrange
        var number = 1234567890;
        var card = new Card
        {
            Id = 1,
            CardNumber = number,
            Status = Status.Active
        };

        _cardRepositoryMock.Setup(x => x.GetCardByNumberAsync(It.IsAny<long>())).ReturnsAsync(card);

        // Act
        var result = _service.GetCardByNumberAsync(number).Result;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Equal(number, result.CardNumber);
        Assert.Equal(Status.Active, result.Status);
    }

    public void GetCardsByAccountAsync_Should_ReturnAccountWithSuccess()
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

        _cardRepositoryMock.Setup(x => x.GetCardsByAccountNumberAsync(It.IsAny<int>())).ReturnsAsync(It.IsAny<IEnumerable<Card>>);

        // Act
        var result = _service.GetCardsByAccountAsync(number).Result;

        // Assert
        Assert.NotNull(result);
    }
}
