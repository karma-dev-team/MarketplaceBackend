using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Payment.Events
{
    public class PurchaseCreated(PurchaseEntity purchase) : BaseEvent
    {
        public PurchaseEntity Purchase { get; set; } = purchase; 
    }
}
