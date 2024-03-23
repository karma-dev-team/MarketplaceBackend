using KarmaMarketplace.Application.Market.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.User.Entities;

namespace KarmaMarketplace.Domain.Market.Entities
{
    public class ReviewEntity : BaseAuditableEntity
    {
        public virtual PurchaseEntity? Purchase { get; set; }

        [Range(1, 5, ErrorMessage = "Рейтинг может быть только 1 и до 5")]
        public int Rating { get; set; }

        [MaxLength(200)]
        public string Text { get; set; } = string.Empty;
        public UserEntity CreatedBy { get; set; } = null!; 
        public ProductEntity Product { get; set; } = null!;
    }
}
