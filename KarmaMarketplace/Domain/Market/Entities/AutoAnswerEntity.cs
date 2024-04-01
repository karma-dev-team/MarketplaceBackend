using KarmaMarketplace.Domain.Market.Events;
using KarmaMarketplace.Domain.Payment.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace KarmaMarketplace.Domain.Market.Entities
{
    public class AutoAnswerEntity : BaseAuditableEntity
    {
        [Required]
        public string Answer { get; set; } = null!;
        [Required]
        public bool IsUsed { get; set; } = false; 
        public DateTime? UsedAt { get; set; }
        [ForeignKey(nameof(ProductEntity))]
        public Guid ProductId { get; set; }
        [ForeignKey(nameof(PurchaseEntity))]
        public Guid? PurchaseId { get; set; }

        public static AutoAnswerEntity Create(Guid productId, string answer)
        {
            var newAnswer = new AutoAnswerEntity();

            newAnswer.ProductId = productId;
            newAnswer.Answer = answer;

            newAnswer.AddDomainEvent(new AutoAnswerCreated(newAnswer)); 

            return newAnswer; 
        }

        public void Use(Guid purchaseId)
        {
            PurchaseId = purchaseId;
            IsUsed = true;

            AddDomainEvent(new AutoAnswerIsUsed(this)); 
        }
    }
}
