using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Application.User.EventHandlers;
using KarmaMarketplace.Application.User.Interactors;
using KarmaMarketplace.Application.User.Interfaces;
using KarmaMarketplace.Application.User.UseCases;
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

            services.AddScoped<CreateNotification>();
            services.AddScoped<GetNotifications>(); 
            services.AddScoped<CreateUser>(); 
            services.AddScoped<UpdateUser>();
            services.AddScoped<DeleteUser>();   
            services.AddScoped<GetUser>();
            services.AddScoped<SendResetCode>(); 
            services.AddScoped<ResetPassword>();
            services.AddScoped<GetUsersList>();
            services.AddScoped<WarnUser>();

            services.AddScoped<UserCreatedHandler>(); 

            return services; 
        }
    }
}
