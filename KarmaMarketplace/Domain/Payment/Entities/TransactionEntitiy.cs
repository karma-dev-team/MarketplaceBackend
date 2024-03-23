using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.Market.ValueObjects;
using KarmaMarketplace.Domain.Payment.Enums;
using KarmaMarketplace.Domain.User.Entities;

namespace KarmaMarketplace.Domain.Payment.Entities
{
    public class TransactionEntity : BaseAuditableEntity
    {

        public Money Amount { get; set; } = null!; 

        public TransactionOperations Operation { get; set; }

        public TransactionDirection Direction { get; set; }

        public TransactionStatusEnum Status { get; set; }

        [ForeignKey("User")]
        public Guid CreatedBy { get; set; }
        public virtual UserEntity User { get; set; } = null!; 

        public DateTime? CompletedAt { get; set; }

        [MaxLength(256)]
        public string StatusDescription { get; set; } = null!; 

        public Money Fee { get; set; } = null!; 
    }
}
