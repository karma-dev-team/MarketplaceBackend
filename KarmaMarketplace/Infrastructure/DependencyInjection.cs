using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Infrastructure.Data;
using KarmaMarketplace.Infrastructure.Data.Intercepters;
using Microsoft.AspNetCore.Identity;
using KarmaMarketplace.Domain.User.Entities;

namespace KarmaMarketplace.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];

            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(connectionString); 
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddSingleton(TimeProvider.System);

            services.AddScoped<PasswordService, PasswordService>(
                x => {
                    return new PasswordService(passwordHasher: new PasswordHasher<UserEntity>());
                }
            );

            services.AddAuthorizationBuilder(); 

            return services;
        }
    }
}
