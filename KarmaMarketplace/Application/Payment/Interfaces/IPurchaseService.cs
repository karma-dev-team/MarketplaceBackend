using KarmaMarketplace.Application.Payment.UseCases;

namespace KarmaMarketplace.Application.Payment.Interfaces
{
    public interface IPurchaseService
    {
        CreatePurchase CreatePurchase();
        ConfirmPurchase ConfirmPurchase(); 
        GetPurchasesList GetPurchasesList();
        UpdatePurchase UpdatePurchase();
        SolveProblem SolveProblem(); 
    }
}
