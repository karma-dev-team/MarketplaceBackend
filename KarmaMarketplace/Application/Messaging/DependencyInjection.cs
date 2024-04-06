using KarmaMarketplace.Application.Messaging.EventsHandlers;
using KarmaMarketplace.Application.Messaging.Interfaces;
using KarmaMarketplace.Application.Messaging.Services;
using KarmaMarketplace.Application.Messaging.UseCases;
using KarmaMarketplace.Domain.User.Events;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Application.Messaging
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMessagingApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<GetChat>();
            services.AddScoped<GetChatsList>();
            services.AddScoped<SendMessage>();
            services.AddScoped<GetChatMessages>();
            services.AddScoped<InitiateProductChat>(); 

            services.AddScoped<IEventSubscriber<UserCreated>, UserCreatedHandler>();
            services.AddScoped<IMessagingService, MessagingService>(); 

            return services;
        }
    }
}
