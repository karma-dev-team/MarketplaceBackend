using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Infrastructure.Data;
using KarmaMarketplace.Infrastructure.Data.Intercepters;
using Microsoft.AspNetCore.Identity;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.Adapters.FileStorage;
using KarmaMarketplace.Infrastructure.Adapters.Mailing;

namespace KarmaMarketplace.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];
            var filesDirectory = configuration["FilesDirectory"];
            var storageType = configuration["StorageType"];

            Guard.Against.Null(storageType, message: "Storage type is not selected"); 

            if (Enum.TryParse(storageType, out StorageTypes type))
            {
                if (type == StorageTypes.Local && string.IsNullOrEmpty(filesDirectory))
                {
                    throw new Exception("Files directory is empty, while storage type is local"); 
                }
            }

            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>(); 
            services.AddScoped<ISaveChangesInterceptor, EventDispatcherInterceptor>(); 

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.EnableDetailedErrors(true);
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(
                    connectionString, 
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                );
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddSingleton(TimeProvider.System);

            services.AddScoped<PasswordService, PasswordService>(
                x => {
                    return new PasswordService(passwordHasher: new PasswordHasher<UserEntity>());
                }
            );

            services.AddMailing(configuration); 

            //services.AddScoped<IFileStorageAdapter, S3StorageAdapter>(); 
            if (type == StorageTypes.Local)
            {
                Guard.Against.Null(filesDirectory, message: "No files directory"); 

                services.AddScoped<IFileStorageAdapter, LocalFileStorageAdapter>(x => new LocalFileStorageAdapter(filesDirectory));
            } 
            services.AddAuthorizationBuilder(); 

            return services;
        }
    }
}
