using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Payment.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Messging.Entities;

namespace KarmaMarketplace.Domain.Payment.Entities
{
    public class PurchaseEntity : BaseAuditableEntity
    {
        // Assuming there's an enum for Currency that was not included in the initial schema
        public CurrencyEnum Currency { get; set; } 

        public decimal Amount { get; set; }

        public virtual WalletEntity Wallet { get; set; } = null!;

        public ProductEntity Product { get; set; } = null!;

        public bool Completed { get; set; } = false; 

        public ChatEntity Chat { get; set; } = null!;

        public PurchaseStatus Status { get; set; }

        public TransactionEntity Transaction { get; set; } = null!;

        [MaxLength(256)]
        public string StatusDescription { get; set; } = null!; 
    }
}
