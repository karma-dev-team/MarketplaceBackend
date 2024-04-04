using KarmaMarketplace.Application.Payment.Interfaces;
using KarmaMarketplace.Application.Payment.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace KarmaMarketplace.Application.Payment.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ServiceProvider _serviceProvider;

        public TransactionService(ServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        public HandleTransaction HandleTransaction()
        {
            return _serviceProvider.GetRequiredService<HandleTransaction>();
        }
        public GetAllTransactions GetAllTransactions()
        {
            return _serviceProvider.GetRequiredService<GetAllTransactions>();
        }
        public EditTransaction EditTransaction()
        {
            return _serviceProvider.GetRequiredService<EditTransaction>();
        }
    }
}
