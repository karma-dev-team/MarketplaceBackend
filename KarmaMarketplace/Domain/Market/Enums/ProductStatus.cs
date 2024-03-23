using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Market.Enums
{
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
