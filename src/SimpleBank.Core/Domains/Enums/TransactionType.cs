using System.Text.Json.Serialization;

namespace SimpleBank.Core.Domains.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransactionType
{
    Purchase = 1,
    Receipt = 2,
    Refund = 3
}