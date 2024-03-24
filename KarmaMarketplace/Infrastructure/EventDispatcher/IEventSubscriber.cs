namespace KarmaMarketplace.Infrastructure.EventDispatcher
{
    public interface IEventSubscriber<TEvent> where TEvent : BaseEvent
    {
        public Task HandleEvent(TEvent @event);
    }
}
