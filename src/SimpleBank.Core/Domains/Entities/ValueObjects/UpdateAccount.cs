using SimpleBank.Core.Domains.Enums;

namespace SimpleBank.Core.Domains.ValueObjects;

public class UpdateAccount
{
    public Status Status { get; init; }
    public string Email { get; init; }
    public UpdateAccount(Status status, string email)
    {
        Status = status;
        Email = email;
    }
}