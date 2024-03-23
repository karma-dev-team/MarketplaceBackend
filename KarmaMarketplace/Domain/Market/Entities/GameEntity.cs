using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Market.Enums;

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
        [ForeignKey("LogoImage")]
        public Guid? LogoID { get; set; }
        public virtual ImageEntity LogoImage { get; set; } = null!; 

        [ForeignKey("BannerImage")]
        public Guid? BannerID { get; set; }
        public virtual ImageEntity BannerImage { get; set; } = null!;
    }
}
