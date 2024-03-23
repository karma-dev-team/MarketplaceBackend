using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Payment.Enums
{
    public enum TransactionDirection
    {
        [Display(Name = "IN")]
        In,
        [Display(Name = "OUT")]
        Out, 
    }
}
