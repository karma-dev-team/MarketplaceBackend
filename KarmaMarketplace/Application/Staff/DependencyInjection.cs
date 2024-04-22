using KarmaMarketplace.Application.Staff.Interfaces;
using KarmaMarketplace.Application.Staff.UseCases;

namespace KarmaMarketplace.Application.Staff
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStaffApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<CreateTicket>();
            services.AddScoped<UpdateTicket>();
            services.AddScoped<DeleteTicket>();
            services.AddScoped<DeleteComment>();
            services.AddScoped<WarnUser>();
            services.AddScoped<CreateComment>(); 

            services.AddScoped<IStaffService, StaffService>();

            return services; 
        }
    } 
}
