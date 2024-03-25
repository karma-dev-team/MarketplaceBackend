using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Files.UseCases;

namespace KarmaMarketplace.Application.Files
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFilesApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<UploadImage>(); 

            services.AddScoped<IFileService, FileService>(); 

            return services; 
        }
    }
}
