using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Tests.Core.Domains.Entities.ValueObjects;

public class CreateAccountTests
{
    [Fact]
    public void GivenCreateAccount_Shoud_SetProperties()
    {
        //Arrange
        var identificationNumber = Guid.NewGuid().ToString();
        var holderName = "Ricardo";
        var birthDate = new DateTime(1997, 12, 03);
        var gender = Gender.Male;
        var email = "ricardo.fulgencio@hotmail.com";

        //Act
        var createAccount = new CreateAccount(identificationNumber, holderName, birthDate, gender, email);

        //Assert
        //createAccount.IdentificationNumber.Should().Be(identificationNumber);
        Assert.Equal(identificationNumber, createAccount.IdentificationNumber);
        Assert.Equal(holderName, createAccount.HolderName);
        Assert.Equal(birthDate, createAccount.BirthDate);
        Assert.Equal(gender, createAccount.Gender);
        Assert.Equal(email, createAccount.Email);
    }
}
