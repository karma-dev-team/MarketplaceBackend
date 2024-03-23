using KarmaMarketplace.Application.User.EventHandlers;
using KarmaMarketplace.Application.User.Interfaces;
using KarmaMarketplace.Domain.User.Events;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Application.User
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEventSubscriber<UserCreated>, UserCreatedSubsciber>();

            return services; 
        }
    }
}
