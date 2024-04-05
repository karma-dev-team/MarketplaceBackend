using System.Text.Json.Serialization;

namespace KarmaMarketplace.Infrastructure.Adapters.Payment
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentProviders
    {
        BankCardRu, 
        Balance, 
        Test
    }
}
