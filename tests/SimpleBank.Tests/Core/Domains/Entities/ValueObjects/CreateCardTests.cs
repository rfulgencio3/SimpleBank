using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Tests.Core.Domains.Entities.ValueObjects;

public class CreateCardTests
{
    [Fact]
    public void GivenCreateCard_Shoud_SetProperties()
    {
        //Arrange
        var displayName = "Lewis Hamilton";
        var cardType = CardType.Gold;

        //Act
        var createCard = new CreateCard(displayName, cardType);

        //Assert
        Assert.Equal(displayName, createCard.DisplayName);
        Assert.Equal(cardType, createCard.CardType);
    }
}
