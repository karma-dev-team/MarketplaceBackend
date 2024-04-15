using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files;
using KarmaMarketplace.Application.Market;
using KarmaMarketplace.Application.Messaging;
using KarmaMarketplace.Application.Messaging.Interfaces;
using KarmaMarketplace.Application.Payment;
using KarmaMarketplace.Application.Payment.EventHandlers;
using KarmaMarketplace.Application.Staff;
using KarmaMarketplace.Application.User;
using KarmaMarketplace.Application.User.Interfaces;
using KarmaMarketplace.Domain.Payment.Events;
using KarmaMarketplace.Domain.User.Events;
using KarmaMarketplace.Infrastructure;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KarmaMarketplace.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.AddScoped<IAccessPolicy, AccessPolicy>(); 
            services.AddUserApplicationServices();
            services.AddMarketApplicationServices();
            services.AddPaymentApplicationServices(); 
            services.AddMessagingApplicationServices(); 
            services.AddFilesApplicationServices();
            services.AddStaffApplicationServices();

            return services; 
        }
    }
}
