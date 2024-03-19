using Microsoft.OpenApi.Any;

namespace NameApp.Infrastructure.EventDispatcher
{
    public interface IEventDispatcher
    {
        public void RegisterEventSubscriber<TEventSubscriber>(TEventSubscriber eventSubscriber)
            where TEventSubscriber : IEventSubscriber<BaseEvent>;
        public void AddListener<TEvent>(Action<TEvent> listener) where TEvent : BaseEvent;
        public void RemoveListener<TEvent>(Action<TEvent> listener) where TEvent : BaseEvent;
        public void Dispatch<TEvent>(TEvent @event) where TEvent : BaseEvent;
    }
}
