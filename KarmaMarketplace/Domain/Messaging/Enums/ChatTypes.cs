using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KarmaMarketplace.Domain.Messaging.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ChatTypes
    {
        [Display(Name = "PRIVATE")]
        Private,
        [Display(Name = "SUPPORT")]
        Support, 
    }
}
