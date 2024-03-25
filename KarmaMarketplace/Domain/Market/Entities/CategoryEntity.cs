using KarmaMarketplace.Application.Market.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Market.Entities
{
    public class CategoryEntity : BaseAuditableEntity
    {
        [Required, MaxLength(256)]
        public string CategoryName { get; set; } = null!;

        public ICollection<OptionEntity> Options { get; set; } = null!; 

        [ForeignKey("Game")]
        public Guid GameID { get; set; } 
        public virtual GameEntity Game { get; set; } = null!; 

        [Required, MaxLength(256)]
        public string Slug { get; set; } = null!; 
    }
}
