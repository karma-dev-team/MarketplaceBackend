using KarmaMarketplace.Domain.Market.ValueObjects;
using KarmaMarketplace.Domain.User.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaMarketplace.Domain.Payment.Entities
{
    public class WalletEntity : BaseAuditableEntity
    {
        [ForeignKey("User")]
        public Guid UserID { get; set; }
        public virtual UserEntity User { get; set; } = null!; 
        public Money Frozen { get; set; } = new(0, Enums.CurrencyEnum.RussianRuble);
        public Money AvailableBalance { get; set; } = new(0, Enums.CurrencyEnum.RussianRuble); 
        public Money PendingIncome { get; set; } = new(0, Enums.CurrencyEnum.RussianRuble);
        public bool Blocked { get; set; } = false;
    }
}
