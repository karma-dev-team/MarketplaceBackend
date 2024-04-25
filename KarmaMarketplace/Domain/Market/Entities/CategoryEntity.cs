using KarmaMarketplace.Application.Market.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.Market.Events;

namespace KarmaMarketplace.Domain.Market.Entities
{
    public class CategoryEntity : BaseAuditableEntity
    {
        [Required, MaxLength(256)]
        public string Name { get; set; } = null!;

        [Required]
        public ICollection<OptionEntity> Options { get; set; } = null!; 

        [ForeignKey(nameof(GameEntity))]
        public Guid GameID { get; set; } 

        [Required, MaxLength(256)]
        public string Slug { get; set; } = null!;

        private static string GenerateSlug(Guid id, string name)
        {
            return id.ToString().Skip(0).Take(8) + name;  
        }

        public static CategoryEntity Create(
            string name, 
            ICollection<OptionEntity> options, 
            Guid gameId, 
            string? slug)
        {
            var id = Guid.NewGuid();

            var categorySlug = slug ?? GenerateSlug(id, name);

            var newCategory = new CategoryEntity() { 
                Id = id,
                Name = name, 
                Options = options,
                GameID = gameId,
                Slug = categorySlug, 
            };

            newCategory.AddDomainEvent(new CategoryCreated(newCategory)); 

            return newCategory; 
        }
    }
}
