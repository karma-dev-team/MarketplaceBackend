using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.AspNetCore.Components;

namespace KarmaMarketplace.Infrastructure.Data.Intercepters
{
    public class EventDispatcherInterceptor : SaveChangesInterceptor
    {
        private readonly IEventDispatcher _dispatcher;

        public EventDispatcherInterceptor(IEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();

            return base.SavingChanges(eventData, result);

        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvents(eventData.Context);

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEvents(DbContext? context)
        {
            if (context == null) return;

            var entities = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());

            // Note: if any event subscribers raise error, then transaction would halt! 
            foreach (var domainEvent in domainEvents)
                _dispatcher.Dispatch(domainEvent);
        }
    }
}
