using KarmaMarketplace.Application.Files.UseCases;

namespace KarmaMarketplace.Application.Files.Interfaces
{
    public interface IFileService
    {
        UploadFile UploadFile();
        UploadFiles UploadFiles(); 
        DeleteFile DeleteFile();
    }
}
