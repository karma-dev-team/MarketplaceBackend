using KarmaMarketplace.Infrastructure.EventDispatcher;
using System.Reflection;

namespace KarmaMarketplace.Application
{
    public static class Configuration
    {
        public static IApplicationBuilder UseEventDispatcher(this IApplicationBuilder app)
        {
            var eventDispatcher = (EventDispatcher)app.ApplicationServices.GetRequiredService<IEventDispatcher>();

            // Resolve and add event listeners within the scope of a request
            using (var scope = app.ApplicationServices.CreateScope())
            {
                eventDispatcher.RegisterEventSubscribers(Assembly.GetExecutingAssembly(), scope);
            }

            return app; 
        }
    }
}
