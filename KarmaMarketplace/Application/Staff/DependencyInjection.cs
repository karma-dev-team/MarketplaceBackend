using KarmaMarketplace.Application.Staff.UseCases;

namespace KarmaMarketplace.Application.Staff
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStaffApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<CreateTicket>();

            return services; 
        }
    } 
}
