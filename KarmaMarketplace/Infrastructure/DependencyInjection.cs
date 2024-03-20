using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Infrastructure.Data;
using KarmaMarketplace.Infrastructure.Data.Intercepters; 

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

            services.AddAuthorizationBuilder(); 

            return services;
        }
    }
}
