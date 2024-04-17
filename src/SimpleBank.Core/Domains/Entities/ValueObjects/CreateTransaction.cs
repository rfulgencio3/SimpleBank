using SimpleBank.Core.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleBank.Core.Domains.ValueObjects;

public class CreateTransaction
{
    [Required(ErrorMessage = "Field 'TransactionType' is required.")]
    public TransactionType TransactionType { get; set; }
    [Required(ErrorMessage = "Field 'Amount' is required.")]
    public decimal Amount { get; set; }
    public CreateTransaction(TransactionType transactionType, decimal amount)
    {
        TransactionType = transactionType;
        Amount = amount;
    }
}