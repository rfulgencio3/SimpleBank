using System.Text.Json.Serialization;

namespace SimpleBank.Core.Domains.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CardType
{
    Simple = 1,
    Silver = 2,
    Gold = 3
}