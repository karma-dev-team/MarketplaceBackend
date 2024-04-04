using KarmaMarketplace.Application.Payment.UseCases;

namespace KarmaMarketplace.Application.Payment.Interfaces
{
    public interface IPurchaseService
    {
        CreatePurchase CreatePurchase();
        ConfirmPurchase ConfirmPurchase(); 
        GetPurchasesList GetPurchases();
        UpdatePurchase EditPurchase();
        SolveProblem SolveProblem(); 
    }
}
