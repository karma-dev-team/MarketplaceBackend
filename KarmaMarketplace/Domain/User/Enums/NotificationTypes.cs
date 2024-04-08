using System.Text.Json.Serialization;

namespace KarmaMarketplace.Domain.User.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum NotificationTypes
    {
        Other, 
        Purchase, 
        Buy, 
        Wallet, 
        Withdraw, 
        System,
    }
}
