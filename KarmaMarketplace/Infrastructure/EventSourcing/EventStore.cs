using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreContext _context;
        private readonly IUser _user; 

        public EventStore(IEventStoreContext context, IUser user)
        {
            _context = context;
            _user = user; 
        }

        public async Task StoreEvent<TEvent>(TEvent @event) where TEvent : BaseEvent
        {
            var storedEvent = new StoredEvent
            {
                EventId = Guid.NewGuid(),
                ByUserId = _user.Id, 
                EventType = typeof(TEvent).Name,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(@event)
            };

            await _context.Events.AddAsync(storedEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StoredEvent>> GetEvents(BaseEvent _eventType, DateTime? from = null, DateTime? to = null)
        {
            var eventType = _eventType.GetType().Name; 
            IQueryable<StoredEvent> query = _context.Events.Where(e => e.EventType == eventType);

            if (from != null)
            {
                query = query.Where(e => e.Timestamp >= from);
            }

            if (to != null)
            {
                query = query.Where(e => e.Timestamp <= to);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<StoredEvent>> GetAllEvents(DateTime? from = null, DateTime? to = null)
        {
            IQueryable<StoredEvent> query = _context.Events;

            if (from != null)
            {
                query = query.Where(e => e.Timestamp >= from);
            }

            if (to != null)
            {
                query = query.Where(e => e.Timestamp <= to);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<StoredEvent>> GetEventsByAggregateId(BaseEvent _eventType, string aggregateId) 
        {
            var eventType = _eventType.GetType().Name;
            return await _context.Events
                .Where(e => e.EventType == eventType && e.Data.Contains($"\"AggregateId\": \"{aggregateId}\""))
                .ToListAsync();
        }
    }
}
