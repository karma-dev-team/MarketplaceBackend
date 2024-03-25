using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files;
using KarmaMarketplace.Application.Market;
using KarmaMarketplace.Application.Messaging;
using KarmaMarketplace.Application.Payment;
using KarmaMarketplace.Application.User;
using KarmaMarketplace.Application.User.EventHandlers;
using KarmaMarketplace.Application.User.Interfaces;
using KarmaMarketplace.Domain.User.Events;
using KarmaMarketplace.Infrastructure;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using System.Reflection;

namespace KarmaMarketplace.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var eventDispatcher = new EventDispatcher();

            eventDispatcher.RegisterEventSubscribers(Assembly.GetExecutingAssembly());

            services.AddSingleton<IEventDispatcher, EventDispatcher>(x => { return eventDispatcher; });
            services.AddScoped<IAccessPolicy, AccessPolicy>(); 
            services.AddUserApplicationServices();
            services.AddMarketApplicationServices();
            services.AddPaymentApplicationServices(); 
            services.AddMessagingApplicationServices();
            services.AddFilesApplicationServices(); 

            return services; 
        }
    }
}
