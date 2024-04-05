using KarmaMarketplace.Application.Payment.Interfaces;
using KarmaMarketplace.Application.Payment.UseCases;

namespace KarmaMarketplace.Application.Payment.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IServiceProvider _serviceProvider;

        public TransactionService(IServiceProvider serviceProvider) {
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

        public HandleGatewayResult HandleGatewayResult()
        {
            return _serviceProvider.GetRequiredService<HandleGatewayResult>();
        }
    }
}
