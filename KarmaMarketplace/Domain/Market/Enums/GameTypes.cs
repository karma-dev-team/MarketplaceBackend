using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Market.Enums
{
    public enum GameTypes
    {
        [Display(Name = "GAME")]
        Game,
        [Display(Name = "APPLICATION")]
        Application
    }
}
