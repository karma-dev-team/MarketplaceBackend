namespace NameApp.Infrastructure.EventDispatcher
{
    public interface IEventSubscriber<TEvent> where TEvent : BaseEvent
    {
        public void HandleEvent(TEvent @event);
    }
}
