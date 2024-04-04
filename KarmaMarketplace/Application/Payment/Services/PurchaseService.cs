using KarmaMarketplace.Application.Payment.Interfaces;
using KarmaMarketplace.Application.Payment.UseCases;

namespace KarmaMarketplace.Application.Payment.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ServiceProvider _serviceProvider;

        public PurchaseService(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CreatePurchase CreatePurchase()
        {
            return _serviceProvider.GetRequiredService<CreatePurchase>();
        }
        public ConfirmPurchase ConfirmPurchase() 
        {
            return _serviceProvider.GetRequiredService<ConfirmPurchase>();
        }
        public GetPurchases GetPurchases()
        {
            return _serviceProvider.GetRequiredService<GetPurchases>();
        }
        public EditPurchase EditPurchase()
        {
            return _serviceProvider.GetRequiredService<EditPurchase>();
        }
        public SolveProblem SolveProblem()
        {
            return _serviceProvider.GetRequiredService<SolveProblem>();
        }
    }
}
