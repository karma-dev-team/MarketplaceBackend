using Microsoft.OpenApi.Any;

namespace KarmaMarketplace.Infrastructure.EventDispatcher
{
    public interface IEventDispatcher
    {
        public void RegisterEventSubscriber<TEvent>(IEventSubscriber<TEvent> eventSubscriber) where TEvent : BaseEvent;
        public void AddListener<TEvent>(Action<TEvent> listener) where TEvent : BaseEvent;
        public void RemoveListener<TEvent>(Action<TEvent> listener) where TEvent : BaseEvent;
        public Task Dispatch<TEvent>(TEvent @event) where TEvent : BaseEvent;
    }
}
