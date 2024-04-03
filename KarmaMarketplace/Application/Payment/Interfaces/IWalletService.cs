namespace KarmaMarketplace.Application.Payment.Interfaces
{
    public interface IWalletService
    {
        BalanceOperation BalanceOperation();
        GetWallet GetWallet(); 
    }
}
