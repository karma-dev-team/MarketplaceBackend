using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.Market.ValueObjects;
using KarmaMarketplace.Domain.Payment.Enums;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.Payment.Events;

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
        public virtual UserEntity CreatedByUser { get; set; } = null!; 

        public DateTime? CompletedAt { get; set; }

        [MaxLength(256)]
        public string StatusDescription { get; set; } = null!; 

        public Money Fee { get; set; } = null!; 

        // Может сущестовать только при завершении транзакции!! 
        public TransactionProviderEntity? Provider {  get; set; }
        public TransactionPropsEntity? Props { get; set; } 

        public static TransactionEntity Create(
            Money value,
            Money fee,
            TransactionOperations operation,
            TransactionDirection direction,
            TransactionProviderEntity provider,
            UserEntity user)
        {
            var transaction = new TransactionEntity
            {
                CreatedByUser = user,
                Fee = fee,
                Amount = value,
                Status = TransactionStatusEnum.Pending,
                Provider = provider,
                Direction = direction,
                Operation = operation
            };
            transaction.AddDomainEvent(new TransactionCreated(transaction));

            return transaction; 
        }

        public void Confirm()
        {
            CompletedAt = DateTime.Now;
            Status = TransactionStatusEnum.Confirmed;
            AddDomainEvent(new TransactionConfirmed(this));
        }

        public void Complete(TransactionStatusEnum status)
        {
            CompletedAt = DateTime.Now;
            Status = status;
        }
    }
}
