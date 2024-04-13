using SimpleBank.Core.Domains.Enums;
using System.ComponentModel.DataAnnotations;

namespace SimpleBank.Core.Domains.DTOs;

public record CreateTransactionDTO
{
    [Required(ErrorMessage = "Field 'TransactionType' is required.")]
    public TransactionType TransactionType { get; set; }
    [Required(ErrorMessage = "Field 'Amount' is required.")]
    public decimal Amount { get; set; }
}