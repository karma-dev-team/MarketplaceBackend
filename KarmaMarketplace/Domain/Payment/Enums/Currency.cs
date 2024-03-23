using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Payment.Enums
{
    public enum CurrencyEnum
    {
        [Display(Name = "RUB")]
        RussianRuble,
        [Display(Name = "USD")]
        Dollar
    }
}
