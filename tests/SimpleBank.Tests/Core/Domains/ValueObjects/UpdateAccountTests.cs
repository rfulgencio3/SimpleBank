using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Tests.Core.Domains.ValueObjects;

public class UpdateAccountTests 
{
    [Fact]
    public void GivenUpdateAccountWithActiveStatus_Shoud_SetProperties()
    {
        //Arrange
        var status = Status.Active;
        var email = "testemail@gmail.com";

        //Act
        var updateAccount = new UpdateAccount(status, email);

        //Assert
        Assert.Equal(status, updateAccount.Status);
        Assert.Equal(email, updateAccount.Email);
    }

    [Fact]
    public void GivenUpdateAccountWithCanceledStatus_Shoud_SetProperties()
    {
        //Arrange
        var status = Status.Canceled;
        var email = "testemail@gmail.com";

        //Act
        var updateAccount = new UpdateAccount(status, email);

        //Assert
        Assert.Equal(status, updateAccount.Status);
        Assert.Equal(email, updateAccount.Email);
    }
    [Fact]
    public void GivenUpdateAccountWithBlockedStatus_Shoud_SetProperties()
    {
        //Arrange
        var status = Status.Blocked;
        var email = "testemail@gmail.com";

        //Act
        var updateAccount = new UpdateAccount(status, email);

        //Assert
        Assert.Equal(status, updateAccount.Status);
        Assert.Equal(email, updateAccount.Email);
    }
}
