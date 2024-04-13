using KarmaMarketplace.Application.Payment.UseCases;

namespace KarmaMarketplace.Application.Payment.Interfaces
{
    public interface ITransactionService
    {
        HandleTransaction HandleTransaction();
        GetTransactionsList GetTransactionsList();
        UpdateTransaction UpdateTransaction(); 
        GetTransactionProviders GetTransactionProviders();
        HandleGatewayResult HandleGatewayResult();
    }
}
