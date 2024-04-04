using KarmaMarketplace.Application.Payment.UseCases;

namespace KarmaMarketplace.Application.Payment.Interfaces
{
    public interface ITransactionService
    {
        HandleTransaction HandleTransaction();
        GetTransactionsList GetAllTransactions();
        UpdateTransaction EditTransaction(); 
        GetTransactionProviders GetTransactionProviders();
    }
}
