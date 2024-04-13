using System.Text.Json.Serialization;

namespace SimpleBank.Core.Domains.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Status
{
    Active = 1,
    Blocked = 2,
    Canceled = 9
}