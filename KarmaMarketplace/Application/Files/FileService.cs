using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Files.UseCases;

namespace KarmaMarketplace.Application.Files
{
    public class FileService : IFileService
    {
        private readonly IServiceProvider ServiceProvider;

        public FileService(
            IServiceProvider serviceProvider
        )
        {
            ServiceProvider = serviceProvider;
        }

        public UploadFile UploadFile()
        {
            return ServiceProvider.GetRequiredService<UploadFile>();
        }

        public UploadFiles UploadFiles()
        {
            return ServiceProvider.GetRequiredService<UploadFiles>();
        }

        public DeleteFile DeleteFile()
        {
            return ServiceProvider.GetRequiredService<DeleteFile>();
        }
    }
}
