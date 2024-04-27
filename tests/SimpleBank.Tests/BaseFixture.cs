using AutoBogus;
using SimpleBank.Core.Domains.Entities;
using SimpleBank.Core.Domains.Enums;

namespace SimpleBank.Tests;

public class BaseFixture
{
    public Account GenerateAccount()
    {
        return new AutoFaker<Account>()
            .RuleFor(x => x.Gender, f => f.PickRandom<Gender>())
            .RuleFor(x => x.HolderName, f => f.Name.FullName())
            .RuleFor(x => x.Email, f => f.Internet.Email())
            .RuleFor(x => x.BirthDate, f => f.Date.Between(DateTime.Today.AddYears(-70), DateTime.Today.AddYears(-18)))
            .Generate();
    }
}
