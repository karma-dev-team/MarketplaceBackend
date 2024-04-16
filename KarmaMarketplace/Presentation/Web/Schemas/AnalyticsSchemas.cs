using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Presentation.Web.Schemas
{
    public class UserAnalyticsSchema 
    {
        [Required]
        public decimal AvarageRating { get; set; }
    }
}
