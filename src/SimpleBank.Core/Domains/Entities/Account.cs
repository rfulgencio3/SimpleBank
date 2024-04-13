using SimpleBank.Core.Domains.DTOs;
using SimpleBank.Core.Domains.Enums;
using System.Reflection.PortableExecutable;

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

    public Account() { }
    public Account FromCreateAccount(CreateAccountDTO account)
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

    public Account FromUpdateAccount(UpdateAccountDTO account)
    {
        return new()
        {
            Status = account.Status,
            Email = account.Email,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
