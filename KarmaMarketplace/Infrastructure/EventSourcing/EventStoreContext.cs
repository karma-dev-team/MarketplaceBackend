using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.EventSourcing
{
    public class EventStoreContext : DbContext, IEventStoreContext
    {
        public DbSet<StoredEvent> Events { get; set; }

        public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
