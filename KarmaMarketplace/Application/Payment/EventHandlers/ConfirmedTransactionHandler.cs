using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Messaging.Interfaces;
using KarmaMarketplace.Domain.Payment.Enums;
using KarmaMarketplace.Domain.Payment.Events;
using KarmaMarketplace.Infrastructure.Data.Queries;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Payment.EventHandlers
{
    public class ConfirmedTransactionHandler : IEventSubscriber<ConfirmedTransaction>
    {
        private IMessagingService _messaging; 
        private IApplicationDbContext _context;

        public ConfirmedTransactionHandler(IMessagingService messaging, IApplicationDbContext dbContext) {
            _messaging = messaging;
            _context = dbContext;
        }

        public async Task HandleEvent(ConfirmedTransaction paymentEvent)
        {
            if (paymentEvent.Transaction.Direction == TransactionDirection.Out 
                    && paymentEvent.Transaction.Operation == TransactionOperations.Buy) {
                var purchase = await _context.Purchases
                    .IncludeStandard()
                    .FirstOrDefaultAsync(x => x.Transaction.Id == paymentEvent.Transaction.Id);

                Guard.Against.Null(purchase, message: "Purchase is not bound error");

                await _messaging.InitiateProductChat().Execute(); 
            }
        }
    }
}
