using SimpleBank.Core.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleBank.Core.Domains.DTOs;

public record CreateCardDTO
{
    [Required(ErrorMessage = "Field 'DisplayName' is required.")]
    public string DisplayName { get; set; }
    [Required(ErrorMessage = "Field 'CardType' is required.")]
    public CardType CardType { get; set; }
}