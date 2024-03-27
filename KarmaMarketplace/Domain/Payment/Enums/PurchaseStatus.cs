using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KarmaMarketplace.Domain.Payment.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PurchaseStatus
    {
        [Display(Name = "CHATTING")]
        Chatting,

        [Display(Name = "PROCESSING")]
        Processing,

        [Display(Name = "FAILED")]
        Failed,

        [Display(Name = "SUCCESS")]
        Success,

        [Display(Name = "REJECTED")]
        Rejected,

        [Display(Name = "EXPIRED")]
        Expired,
    }
}
