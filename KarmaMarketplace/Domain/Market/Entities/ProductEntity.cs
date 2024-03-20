using KarmaMarketplace.Application.Market.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using KarmaMarketplace.Domain.Market.ValueObjects;

namespace KarmaMarketplace.Domain.Market.Entities
{
    public class ProductEntity : BaseAuditableEntity
    {
        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public virtual UserEntity User { get; set; } = null!;

        [ForeignKey("Category")]
        public Guid CategoryID { get; set; }
        public virtual CategoryEntity Category { get; set; } = null!;

        [Required, MaxLength(256)]
        public string Name { get; set; } = null!; 

        [Required, MaxLength(256)]
        public string Slug { get; set; } = null!;

        // Assuming Money is a decimal. If Money is a complex type, adjust accordingly.
        public Money? DiscountPrice { get; set; }

        public Money BasePrice { get; set; }

        public string Description { get; set; } = null!; 

        public ProductStatus Status { get; set; } 

        public UserEntity? BuyerUser { get; set; }

        public UserEntity CreatedByUser { get; set; } = null!; 

        [Column(TypeName = "jsonb")]
        public string Attributes { get; set; } = null!; 
    }
}
