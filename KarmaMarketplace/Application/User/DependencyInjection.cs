using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Application.User.EventHandlers;
using KarmaMarketplace.Application.User.Interactors;
using KarmaMarketplace.Application.User.Interfaces;
using KarmaMarketplace.Domain.User.Entities;
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

            services.AddScoped<CreateUser, CreateUser>(); 
            services.AddScoped<UpdateUser, UpdateUser>();
            services.AddScoped<DeleteUser, DeleteUser>();   
            services.AddScoped<GetUser, GetUser>();
            services.AddScoped<GetUsersList, GetUsersList>(); 

            return services; 
        }
    }
}
