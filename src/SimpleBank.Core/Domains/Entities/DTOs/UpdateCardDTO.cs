using SimpleBank.Core.Domains.Enums;

namespace SimpleBank.Core.Domains.DTOs;

public record UpdateCardDTO
{
    public Status Status { get; init; }
}