using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Files.UseCases;

namespace KarmaMarketplace.Application.Files
{
    public class FileService : IFileService
    {
        private readonly IServiceProvider ServiceProvider;

        public UserService(
            IServiceProvider serviceProvider
        )
        {
            ServiceProvider = serviceProvider;
        }

        public UploadImage UploadImage()
        {
            return ServiceProvider.GetRequiredService<UploadImage>();
        }
    }
}
