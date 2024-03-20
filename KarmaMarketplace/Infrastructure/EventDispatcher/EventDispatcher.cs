using System.Reflection;

namespace KarmaMarketplace.Infrastructure.EventDispatcher
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly Dictionary<Type, List<Delegate>> eventListeners = new();

        public void RegisterEventSubscribers(Assembly assembly)
        {
            var types = assembly.GetTypes();
            var eventSubscriberTypes = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && ImplementsEventSubscriberInterface(type));

            foreach (var eventSubscriberType in eventSubscriberTypes)
            {
                var eventSubscriber = Activator.CreateInstance(eventSubscriberType);
                RegisterEventSubscriber((IEventSubscriber<BaseEvent>)eventSubscriber);
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
                    eventListeners[eventType] = [];
                }

                var eventHandler = CreateEventHandlerDelegate(eventSubscriber, eventType);
                eventListeners[eventType].Add(eventHandler);
            }
        }

        public void AddListener<TEvent>(Action<TEvent> listener) where TEvent : BaseEvent
        {
            Type eventType = typeof(TEvent);
            if (!eventListeners.ContainsKey(eventType))
            {
                eventListeners[eventType] = [];
            }

            eventListeners[eventType].Add(listener);
        }

        public void RemoveListener<TEvent>(Action<TEvent> listener) where TEvent : BaseEvent
        {
            Type eventType = typeof(TEvent);
            if (eventListeners.ContainsKey(eventType))
            {
                eventListeners[eventType].Remove(listener);
            }
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : BaseEvent
        {
            Type eventType = typeof(TEvent);
            if (eventListeners.ContainsKey(eventType))
            {
                foreach (var listener in eventListeners[eventType].ToList())
                {
                    ((Action<TEvent>)listener)(@event);
                }
            }
        }

        private bool ImplementsEventSubscriberInterface(Type type)
        {
            var eventSubscriberInterface = typeof(IEventSubscriber<>);
            var result = eventSubscriberInterface.IsAssignableFrom(type);
            return result;
        }

        private IEnumerable<Type> GetEventTypes(Type eventSubscriberType)
        {
            var eventSubscriberInterface = typeof(IEventSubscriber<>);
            return eventSubscriberType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == eventSubscriberInterface)
                .Select(i => i.GetGenericArguments()[0]);
        }

        private Delegate CreateEventHandlerDelegate(object eventSubscriber, Type eventType)
        {
            var handleEventMethod = typeof(IEventSubscriber<>).MakeGenericType(eventType)
                .GetMethod("HandleEvent");

            return Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(eventType), eventSubscriber, handleEventMethod);
        }
    }
}
