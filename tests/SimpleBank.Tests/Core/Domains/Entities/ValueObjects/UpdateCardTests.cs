using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Tests.Core.Domains.Entities.ValueObjects;

public class UpdateCardTests
{
    [Fact]
    public void GivenUpdateCardWithActiveStatus_Shoud_SetProperties()
    {
        //Arrange
        var status = Status.Active;

        //Act
        var updateAccount = new UpdateCard(status);

        //Assert
        Assert.Equal(status, updateAccount.Status);
    }

    [Fact]
    public void GivenUpdateCardWithCanceledStatus_Shoud_SetProperties()
    {
        //Arrange
        var status = Status.Canceled;

        //Act
        var updateCard = new UpdateCard(status);

        //Assert
        Assert.Equal(status, updateCard.Status);
    }

    [Fact]
    public void GivenUpdateCardWithBlockedStatus_Shoud_SetProperties()
    {
        //Arrange
        var status = Status.Blocked;

        //Act
        var updateCard = new UpdateCard(status);

        //Assert
        Assert.Equal(status, updateCard.Status);
    }
}
