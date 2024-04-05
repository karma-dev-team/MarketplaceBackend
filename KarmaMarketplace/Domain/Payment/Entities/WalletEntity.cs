using KarmaMarketplace.Domain.Market.ValueObjects;
using KarmaMarketplace.Domain.Payment.Events;
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

        public static WalletEntity Create(UserEntity user)
        {
            var wallet = new WalletEntity();

            wallet.User = user;
            wallet.UserID = user.Id;

            wallet.AddDomainEvent(new WalletCreated(wallet)); 

            return wallet; 
        }

        public void AddBalance(Money amount)
        {
            AvailableBalance.Amount += amount.Amount;

            AddDomainEvent(new BalanceChanged(this, amount)); 
        }
    }
}
