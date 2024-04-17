using SimpleBank.Core.Domains.Enums;

namespace SimpleBank.Core.Domains.ValueObjects;

public class UpdateCard
{
    public UpdateCard(Status status)
    {
        Status = status;
    }
    public Status Status { get; init; }
}