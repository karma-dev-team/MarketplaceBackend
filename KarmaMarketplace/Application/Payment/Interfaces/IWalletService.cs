using KarmaMarketplace.Application.Payment.UseCases;

namespace KarmaMarketplace.Application.Payment.Interfaces
{
    public interface IWalletService
    {
        BalanceOperation BalanceOperation();
        GetWallet GetWallet();
        BlockWallet BlockWallet(); 
    }
}
