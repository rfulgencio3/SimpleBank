using AutoBogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;
using System.Security.Principal;

namespace SimpleBank.Tests.Core.Domain;

public class AccountTests
{
    public AccountTests()
    {           
    }

    [Fact]
    public void FromCreateAccount_Should_SetPropieties()
    {
        // Arrange
        var createAccount = AutoFaker.Generate<CreateAccount>();

        // Act
        var result = new Account().FromCreateAccount(createAccount);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Status.Active);
        result.Balance.Should().Be(0.00M);
    }
}
