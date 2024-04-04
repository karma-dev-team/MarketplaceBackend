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
        public GetTransactionsList GetAllTransactions()
        {
            return _serviceProvider.GetRequiredService<GetTransactionsList>();
        }
        public UpdateTransaction EditTransaction()
        {
            return _serviceProvider.GetRequiredService<UpdateTransaction>();
        }

        public GetTransactionProviders GetTransactionProviders()
        {
            return _serviceProvider.GetRequiredService<GetTransactionProviders>();
        }
    }
}
