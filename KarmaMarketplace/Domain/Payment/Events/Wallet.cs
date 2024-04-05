using KarmaMarketplace.Domain.Market.ValueObjects;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Payment.Events
{
    public class WalletCreated(WalletEntity wallet) : BaseEvent
    {
        public WalletEntity Wallet { get; set; } = wallet;
    }

    public class BalanceChanged(WalletEntity wallet, Money amount ) : BaseEvent
    {
        public WalletEntity Wallet { get; set; } = wallet;
        public Money Balance { get; set; } = amount;
    }
}
