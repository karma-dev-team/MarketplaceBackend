using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Payment.Enums
{
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
