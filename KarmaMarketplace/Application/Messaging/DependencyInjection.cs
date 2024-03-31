using KarmaMarketplace.Application.Messaging.EventsHandlers;
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

            services.AddScoped<IEventSubscriber<UserCreated>, UserCreatedHandler>();


            return services;
        }
    }
}
