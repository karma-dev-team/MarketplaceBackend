using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KarmaMarketplace.Domain.Messging.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MessageTypes
    {
        [Display(Name = "TEXT")]
        Text,
        [Display(Name = "IMAGE")]
        Image,
        [Display(Name = "PURCHASE")]
        Purchase, 
        [Display(Name = "REVIEW")]
        Review
    }
}
