using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Tests.Core.Domains.Entities;

public class AccountTests
{
    public AccountTests()
    {
    }

    [Fact]
    public void FromCreateAccount_Should_SetPropieties()
    {
        //Arrange
        var identificationNumber = "1";
        var holderName = "Ricardo";
        var birthDate = new DateTime(1990, 01, 30);
        var gender = Gender.Male;
        var email = "teste@email.com";

        var createAccount = new CreateAccount(identificationNumber, holderName, birthDate, gender, email);

        //Act
        var account = new Account().FromCreateAccount(createAccount);

        //Assert
        Assert.NotNull(account);
        Assert.Equal(account.IdentificationNumber, createAccount.IdentificationNumber);
        Assert.Equal(account.HolderName, createAccount.HolderName);
        Assert.Equal(account.BirthDate, createAccount.BirthDate);
        Assert.Equal(account.Gender, createAccount.Gender);
        Assert.Equal(account.Email, createAccount.Email);
        Assert.NotNull(account.Balance);
    }

    [Fact]
    public void FromUpdateAccount_Should_SetPropieties()
    {
        //Arrange
        var status = Status.Blocked;
        var email = "teste2@email.com";

        var updateAccount = new UpdateAccount(status, email);

        //Act
        var account = new Account().FromUpdateAccount(updateAccount);

        //Assert
        Assert.NotNull(account);
        Assert.Equal(account.Status, updateAccount.Status);
        Assert.Equal(account.Email, updateAccount.Email);
    }
}
