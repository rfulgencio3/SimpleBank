using SimpleBank.Core.Domains.Enums;

namespace SimpleBank.Core.Domains.DTOs;

public record UpdateAccountDTO
{
    public Status Status { get; init; }
    public string Email { get; init; }
}