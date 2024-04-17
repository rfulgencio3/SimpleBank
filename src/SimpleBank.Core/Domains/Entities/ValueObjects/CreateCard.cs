using SimpleBank.Core.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleBank.Core.Domains.ValueObjects;

public class CreateCard
{
    [Required(ErrorMessage = "Field 'DisplayName' is required.")]
    public string DisplayName { get; set; }
    [Required(ErrorMessage = "Field 'CardType' is required.")]
    public CardType CardType { get; set; }
    public CreateCard(string displayName, CardType cardType)
    {
        DisplayName = displayName;
        CardType = cardType;
    }
}