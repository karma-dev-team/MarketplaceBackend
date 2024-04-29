using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.EventSourcing
{
    public class EventStoreContext : DbContext, IEventStoreContext
    {
        public DbSet<StoredEvent> Events { get; set; }

        public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<StoredEvent>()
                .HasIndex(e => e.EventType)
                .HasDatabaseName("IX_EventType");
            builder.Entity<StoredEvent>()
                .HasIndex(e => e.EventId)
                .HasDatabaseName("IX_EventId");
        }
    }
}
