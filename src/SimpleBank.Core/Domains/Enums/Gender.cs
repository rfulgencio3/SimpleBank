using System.Text.Json.Serialization;

namespace SimpleBank.Core.Domains.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3,
        PreferNotToSay = 4
    } 
}