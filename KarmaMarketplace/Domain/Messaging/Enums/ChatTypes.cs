using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Messaging.Enums
{
    public enum ChatTypes
    {
        [Display(Name = "PRIVATE")]
        Private,
        [Display(Name = "SUPPORT")]
        Support, 
    }
}
