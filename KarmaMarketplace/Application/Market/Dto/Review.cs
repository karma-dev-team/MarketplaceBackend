using KarmaMarketplace.Application.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Market.Dto
{
    public class CreateReviewDto
    {
        [Required]
        public string Text { get; set; } = null!;
        [Required]
        [Range(1, 5, ErrorMessage = "Must be between 1 to 5")]
        public int Rate { get; set; }
        [Required]
        public Guid PuchaseId { get; set; }
        public Guid? ChatID { get; set; }
    }

    public class GetReviewsListDto : InputPagination
    {
        public Guid? ChatID { get; set; }
        public Guid? UserId { get; set; }
    }
}
