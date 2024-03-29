using System.Reflection;

namespace KarmaMarketplace.Infrastructure.EventDispatcher
{

    public class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<Type, List<Delegate>> eventListeners = new();
        private readonly IServiceProvider serviceProvider;

        public EventDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void RegisterEventSubscribers(Assembly assembly)
        {
            var eventSubscriberTypes = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && ImplementsEventSubscriberInterface(type));

            foreach (var eventSubscriberType in eventSubscriberTypes)
            {
                var eventSubscriber = serviceProvider.GetService(eventSubscriberType);
                if (eventSubscriber != null)
                {
                    RegisterEventSubscriber((IEventSubscriber<BaseEvent>)eventSubscriber);
                }
            }
        }

        public void RegisterEventSubscriber<TEventSubscriber>(TEventSubscriber eventSubscriber)
            where TEventSubscriber : IEventSubscriber<BaseEvent>
        {
            var eventTypes = GetEventTypes(eventSubscriber.GetType());

            foreach (var eventType in eventTypes)
            {
                if (!eventListeners.ContainsKey(eventType))
                {
                    eventListeners[eventType] = new List<Delegate>();
                }

                var eventHandler = CreateEventHandlerDelegate(eventSubscriber, eventType);
                eventListeners[eventType].Add(eventHandler);
            }
        }

        public void AddListener<TEvent>(Action<TEvent> listener) where TEvent : BaseEvent
        {
            var eventType = typeof(TEvent);
            if (!eventListeners.ContainsKey(eventType))
            {
                eventListeners[eventType] = new List<Delegate>();
            }

            eventListeners[eventType].Add(listener);
        }

        public void RemoveListener<TEvent>(Action<TEvent> listener) where TEvent : BaseEvent
        {
            var eventType = typeof(TEvent);
            if (eventListeners.ContainsKey(eventType))
            {
                eventListeners[eventType].Remove(listener);
            }
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : BaseEvent
        {
            var eventType = typeof(TEvent);
            if (eventListeners.ContainsKey(eventType))
            {
                foreach (var listener in eventListeners[eventType].ToList())
                {
                    if (listener is Action<TEvent> action)
                    {
                        action(@event);
                    }
                }
            }
        }

        private bool ImplementsEventSubscriberInterface(Type type)
        {
            return type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventSubscriber<>));
        }

        private IEnumerable<Type> GetEventTypes(Type eventSubscriberType)
        {
            return eventSubscriberType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventSubscriber<>))
                .Select(i => i.GetGenericArguments()[0]);
        }

        private Delegate CreateEventHandlerDelegate(object eventSubscriber, Type eventType)
        {
            var handleEventMethod = eventSubscriber.GetType().GetMethod("HandleEvent", new[] { eventType });
            return Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(eventType), eventSubscriber, handleEventMethod, false);
        }
    }
}
