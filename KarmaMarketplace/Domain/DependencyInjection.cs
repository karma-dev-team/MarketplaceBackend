using Microsoft.AspNetCore.Identity;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Services;

namespace KarmaMarketplace.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<PasswordService, PasswordService>(
                x => {
                    return new PasswordService(passwordHasher: new PasswordHasher<UserEntity>());
                }
            );
            services.AddScoped<UserDomainService, UserDomainService>();

            return services; 
        }
    }
}
