using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Market.Enums
{
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
