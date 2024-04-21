using KarmaMarketplace.Domain.Market.Services;

namespace KarmaMarketplace.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ProductDomainService>(); 

            return services; 
        }
    } 
}
