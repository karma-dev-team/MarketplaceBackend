using KarmaMarketplace.Domain.User.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaMarketplace.Domain.Market.Entities
{
    public class ProductViewEntity : BaseAuditableEntity
    {
        [ForeignKey(nameof(ProductEntity))]
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(UserEntity))]
        public Guid UserId { get; set; }
        [Column(TypeName = "jsonb")]
        public string? Info { get; set; } = null!; 

        public ProductViewEntity(Guid productId, Guid userId, string? info) {
            ProductId = productId; 
            UserId = userId;
            Info = info;
        }
    }
}
