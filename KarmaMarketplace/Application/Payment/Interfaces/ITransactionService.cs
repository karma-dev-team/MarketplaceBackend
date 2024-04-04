using KarmaMarketplace.Application.Payment.UseCases;

namespace KarmaMarketplace.Application.Payment.Interfaces
{
    public interface ITransactionService
    {
        HandleTransaction HandleTransaction();
        GetAllTransactions GetAllTransactions();
        EditTransaction EditTransaction(); 
        GetTransactionProviders GetTransactionProviders();
    }
}
