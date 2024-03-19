using NameApp.Application.Common.IoC;
using NameApp.Application.User;
using NameApp.Application.User.EventHandlers;
using NameApp.Application.User.Interfaces;
using NameApp.Domain.User.Events;
using NameApp.Infrastructure.EventDispatcher;
using System.Reflection;

namespace NameApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var eventDispatcher = new EventDispatcher();

            eventDispatcher.RegisterEventSubscribers(Assembly.GetExecutingAssembly());

            services.AddScoped<IEventSubscriber<UserCreated>, UserCreatedSubsciber>();
            services.AddSingleton<IEventDispatcher, EventDispatcher>(x => { return eventDispatcher; });
            services.AddScoped<IUserService, UserService>(); 

            return services; 
        }
    }
}
