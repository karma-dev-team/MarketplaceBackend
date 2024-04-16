using KarmaMarketplace.Application.Market.Dto;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.Market.Events;
using KarmaMarketplace.Domain.Payment.Exceptions;

namespace KarmaMarketplace.Domain.Market.Entities
{
    public class ReviewEntity : BaseAuditableEntity
    {
        [Required]
        public PurchaseEntity Purchase { get; set; }

        [Range(1, 5, ErrorMessage = "Рейтинг может быть только 1 и до 5"), Required]
        public int Rating { get; set; }

        [MaxLength(200)]
        public string Text { get; set; } = string.Empty;
        [Required]
        public UserEntity CreatedBy { get; set; } = null!; 
        [Required]
        public ProductEntity Product { get; set; } = null!;

        public static ReviewEntity Create(
            string text, 
            int rating, 
            UserEntity createdBy, 
            PurchaseEntity purchase, 
            ProductEntity product) 
        {
            if (purchase.Status == Payment.Enums.PurchaseStatus.Success)
            {
                throw new PurchaseIsAlreadyCompleted(purchase.Id); 
            }

            var review = new ReviewEntity()
            {
                CreatedBy = createdBy,
                Text = text,
                Rating = rating,
                Product = product,
                Purchase = purchase
            };

            review.AddDomainEvent(new ReviewCreated(review));

            return review; 
        }
    }
}
