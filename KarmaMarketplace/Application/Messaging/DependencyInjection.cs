using KarmaMarketplace.Application.Messaging.EventsHandlers;
using KarmaMarketplace.Application.Messaging.UseCases;

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

            services.AddScoped<UserCreatedHandler>();

            return services;
        }
    }
}
