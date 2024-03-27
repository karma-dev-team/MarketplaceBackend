using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KarmaMarketplace.Domain.Payment.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionStatusEnum
    {
        [Display(Name = "ROLLED_BACK")]
        RolledBack,

        [Display(Name = "CONFIRMED")]
        Confirmed,

        [Display(Name = "FAILED")]
        Failed,

        [Display(Name = "EXPIRED")]
        Expired,

        [Display(Name = "PENDING")]
        Pending,
    }
}
