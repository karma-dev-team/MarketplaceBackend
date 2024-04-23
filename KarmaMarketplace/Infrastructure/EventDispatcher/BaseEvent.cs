namespace KarmaMarketplace.Infrastructure.EventDispatcher
{
    public abstract class BaseEvent
    {

        public DateTime Timestamp { get; set; }
        public Guid? AggregateId { get; set; }

        protected BaseEvent(Guid? aggregateId = null)
        {
            AggregateId = aggregateId; 
            Timestamp = DateTime.UtcNow;
        }
    }
}
