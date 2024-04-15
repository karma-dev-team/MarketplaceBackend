using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.AspNetCore.Components;

namespace KarmaMarketplace.Infrastructure.Data.Intercepters
{
    public class EventDispatcherInterceptor : SaveChangesInterceptor
    {
        private readonly IEventDispatcher _dispatcher;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public EventDispatcherInterceptor(
            IEventDispatcher dispatcher, ILogger<EventDispatcherInterceptor> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _dispatcher = dispatcher;
            _serviceProvider = serviceProvider;
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
            if (context == null) return ;

            var entities = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            entities.ToList().ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                try
                {
                    var dispatcher = _serviceProvider.GetRequiredService<IEventDispatcher>();
                    await dispatcher.Dispatch(domainEvent);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error dispatching domain event: {domainEvent}");
                    throw; 
                }
            }
        }
    }
}
