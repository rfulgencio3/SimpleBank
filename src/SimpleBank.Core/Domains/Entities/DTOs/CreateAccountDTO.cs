using SimpleBank.Core.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleBank.Core.Domains.DTOs;

public record CreateAccountDTO
{
    public string IdentificationNumber { get; init; }
    [Required(ErrorMessage = "Field 'HolderName' is required.")]
    public string HolderName { get; init; }
    [Required(ErrorMessage = "Field 'BirthDate' is required.")]
    public DateTime BirthDate { get; init; }
    public Gender Gender { get; init; }
    [Required(ErrorMessage = "Field 'Email' is required.")]
    public string Email { get; init; }
}