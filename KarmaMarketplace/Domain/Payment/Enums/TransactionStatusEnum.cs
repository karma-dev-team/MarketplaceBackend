using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Payment.Enums
{
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
