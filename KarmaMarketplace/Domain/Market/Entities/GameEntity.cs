using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using KarmaMarketplace.Domain.Market.Events;

namespace KarmaMarketplace.Domain.Market.Entities
{
    public class GameEntity : BaseAuditableEntity
    {
        [Required, MaxLength(256)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(256)]
        public string? Slug { get; set; } = null!;

        [Required]
        public string? Description { get; set; } = null!;

        [Required]
        public GameTypes Type { get; set; } = GameTypes.Game; 

        [Column(TypeName = "jsonb")]
        public string Tags { get; set; } = null!; 

        // Assuming Logo and Banner are optional foreign keys to the Image table
        [ForeignKey("Logo")]
        public Guid? LogoID { get; set; }
        [Required]
        public virtual FileEntity Logo { get; set; } = null!;

        public ICollection<CategoryEntity> Categories { get; set; } = []; 

        [ForeignKey("Banner")]
        public Guid? BannerID { get; set; }
        public virtual FileEntity Banner { get; set; } = null!;

        public static GameEntity Create(
            string name, 
            string? description, 
            GameTypes type, 
            string tags, 
            FileEntity banner, 
            FileEntity logo)
        {
            var game = new GameEntity();

            game.Name = name;
            game.Description = description;
            game.Type = type;
            game.Tags = tags;
            game.Banner = banner;
            game.Logo = logo;
            game.Slug = Guid.NewGuid().ToString(); 

            game.AddDomainEvent(new GameCreated(game));

            return game;
        }
    }
}
