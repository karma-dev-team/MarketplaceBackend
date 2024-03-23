using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Messging.Enums
{
    public enum MessageTypes
    {
        [Display(Name = "TEXT")]
        Text,
        [Display(Name = "PHOTO")]
        Photo,
        [Display(Name = "PURCHASE")]
        Purchase
    }
}
