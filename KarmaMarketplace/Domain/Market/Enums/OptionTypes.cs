using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KarmaMarketplace.Domain.Market.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OptionTypes
    {

        [Display(Name = "SELECTOR")]
        Selector,

        [Display(Name = "SWITCH")]
        Switch,

        [Display(Name = "RANGE")]
        Range,
    }
}
