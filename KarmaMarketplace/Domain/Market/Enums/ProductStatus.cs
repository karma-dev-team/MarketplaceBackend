using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KarmaMarketplace.Domain.Market.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ProductStatus
    {
        [Display(Name = "PROCESSING")]
        Processing, 

        [Display(Name = "APPROVED")]
        Approved,

        [Display(Name = "DECLINED")]
        Declined,

        [Display(Name = "BLOCKED")]
        Blocked, 

        [Display(Name = "DRAFT")]
        Draft, 

        [Display(Name = "SOLD")]
        Sold, 
    }
}
