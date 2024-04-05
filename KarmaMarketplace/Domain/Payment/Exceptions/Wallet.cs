namespace KarmaMarketplace.Domain.Payment.Exceptions
{
    public class WalletIsBlocked : Exception
    {
        public Guid WalletId { get; set; }

        public WalletIsBlocked(Guid walletId) : base("Wallet is blocked") {
            WalletId = walletId;
        }
    }

    public class NotEnoughMoneyException : Exception
    {
        public Guid WalletId { get; set; }

        public NotEnoughMoneyException(Guid walletId) : base("Wallet is blocked")
        {
            WalletId = walletId;
        }
    }


}
