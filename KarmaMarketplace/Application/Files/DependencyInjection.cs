using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Files.UseCases;

namespace KarmaMarketplace.Application.Files
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFilesApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<UploadFile>();
            services.AddScoped<UploadFiles>(); 
            services.AddScoped<DeleteFile>();
            services.AddScoped<DeleteFiles>();

            services.AddScoped<IFileService, FileService>(); 

            return services; 
        }
    }
}
