namespace KarmaMarketplace.Application.Payment.Interfaces
{
    public interface IPurchaseService
    {
        CreatePurchase CreatePurchase();
        ConfirmPurchase ConfirmPurchase(); 
        GetPurchases GetPurchases();
        EditPurchase EditPurchase();
        SolveProblem SolveProblem(); 
    }
}
