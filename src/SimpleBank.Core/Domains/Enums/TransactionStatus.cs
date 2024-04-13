using System.Text.Json.Serialization;

namespace SimpleBank.Core.Domains.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TransactionStatus
{
    Approved = 1,
    Processed = 2,
    Declined = 3,
    Canceled = 9
}