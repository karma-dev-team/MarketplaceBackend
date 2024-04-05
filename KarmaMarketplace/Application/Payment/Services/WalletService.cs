using KarmaMarketplace.Application.Payment.Interfaces;
using KarmaMarketplace.Application.Payment.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace KarmaMarketplace.Application.Payment.Services
{
    public class WalletService : IWalletService
    {
        private readonly IServiceProvider _serviceProvider;

        public WalletService(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public BalanceOperation BalanceOperation()
        {
            return _serviceProvider.GetRequiredService<BalanceOperation>();
        }
        public GetWallet GetWallet()
        {
            return _serviceProvider.GetRequiredService<GetWallet>();
        }
    }
}
