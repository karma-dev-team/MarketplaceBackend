using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Payment.Events
{
    public class TransactionCreated(TransactionEntity transaction) : BaseEvent
    {
        public TransactionEntity Transaction { get; set; } = transaction; 
    }

    public class TransactionConfirmed(TransactionEntity transaction) : BaseEvent
    {
        public TransactionEntity Transaction { get; set; } = transaction;
    }
}
