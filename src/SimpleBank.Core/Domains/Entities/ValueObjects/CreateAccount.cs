using SimpleBank.Core.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleBank.Core.Domains.ValueObjects;

public class CreateAccount
{
    public string IdentificationNumber { get; init; }
    [Required(ErrorMessage = "Field 'HolderName' is required.")]
    public string HolderName { get; init; }
    [Required(ErrorMessage = "Field 'BirthDate' is required.")]
    public DateTime BirthDate { get; init; }
    public Gender Gender { get; init; }
    [Required(ErrorMessage = "Field 'Email' is required.")]
    public string Email { get; init; }
    public CreateAccount(string identificationNumber, string holderName, DateTime birthDate, Gender gender, string email)
    {
        IdentificationNumber = identificationNumber;
        HolderName = holderName;
        BirthDate = birthDate;
        Gender = gender;
        Email = email;
    }
}