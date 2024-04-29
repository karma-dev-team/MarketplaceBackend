using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KarmaMarketplace.Infrastructure.EventSourcing
{
    public interface IEventStoreContext
    {
        public DbSet<StoredEvent> Events { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        EntityEntry Entry(object entity);
    }
}
