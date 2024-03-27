using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Market.Dto
{
    public class CreateCategoryDto 
    {
        [Required]
        public string Name { get; set; } = null!;
        public ICollection<CreateOption> Options { get; set; } = []; 
        public Guid GameId { get; set; }
    }
}
