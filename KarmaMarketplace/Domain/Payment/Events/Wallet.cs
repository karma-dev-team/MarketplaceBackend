using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.Payment.ValueObjects;
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

    public class RecordWallet(WalletEntity wallet) : BaseEvent
    {
        public WalletEntity Wallet { get; set; } = wallet; 
    }

    public class WalletAmountFrozen(WalletEntity wallet, Money frozen) : BaseEvent
    {
        public Money Frozen { get; set; } = frozen;
        public WalletEntity Wallet { get; set; } = wallet;
    }

    public class WalletUnfrozenAmount(WalletEntity wallet, Money unfrozen) : BaseEvent
    {
        public WalletEntity Wallet { get; set; } = wallet;
        public Money Unfrozen { get; set; } = unfrozen;
    }

    public class WalletBlocked(WalletEntity wallet, string reason) : BaseEvent
    {
        public WalletEntity Wallet { get; set; } = wallet;
        public string Reason { get; set; } = reason; 
    }

    public class ConfirmedTransaction(WalletEntity wallet, TransactionEntity transaction, WalletEntity fromWallet) : BaseEvent
    {
        public WalletEntity Wallet { get; set; } = wallet;
        public TransactionEntity Transaction { get; set; } = transaction; 
        public WalletEntity FromWallet { get; set; } = fromWallet;
    }

    public class WalletBalanceDecreased(WalletEntity wallet, Money decreasedAmount) : BaseEvent
    {
        public Money DecreasedAmount { get; set; } = decreasedAmount;
        public WalletEntity Wallet { get; set; } = wallet;
    }

    public class WalletBalanceIncreased(WalletEntity wallet, Money increasedAmount) : BaseEvent
    {
        public Money IncreasedAmount { get; set; } = increasedAmount;
        public WalletEntity Wallet { get; set; } = wallet;
    }

    public class WalletUnfreezeAmount(WalletEntity wallet, Money unfrozen) : BaseEvent
    {
        public Money Unfrozen { get; set; } = unfrozen; 
        public WalletEntity Wallet { get; set; } = wallet; 
    }
}
