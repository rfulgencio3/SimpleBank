using SimpleBank.Core.Domains.Enums;
using SimpleBank.Core.Domains.ValueObjects;

namespace SimpleBank.Core.Domains.Entities;

public class Account : Base
{
    public Status Status { get; set; }
    public int AccountNumber { get; set; }
    public string IdentificationNumber { get; set; }
    public string HolderName { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public virtual ICollection<Card> Cards { get; set; }

    public Account FromCreateAccount(CreateAccount account)
    {
        return new()
        {
            Status = Status.Active,
            IdentificationNumber = account.IdentificationNumber,
            HolderName = account.HolderName,
            BirthDate = account.BirthDate,
            Gender = account.Gender,
            Email = account.Email,
            Balance = 0.00M,
            CreatedAt = DateTime.UtcNow
        };
    }

    public Account FromUpdateAccount(UpdateAccount account)
    {
        return new()
        {
            Status = account.Status,
            Email = account.Email,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
