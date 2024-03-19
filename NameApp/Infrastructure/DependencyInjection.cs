using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NameApp.Application.Common.Interfaces;
using NameApp.Infrastructure.Data;
using NameApp.Infrastructure.Data.Intercepters; 

namespace NameApp.Infrastructure
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

            //services.AddScoped<ApplicationDbContextInitialiser>();

            return services;
        }
    }
}
