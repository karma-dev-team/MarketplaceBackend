using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;

namespace KarmaMarketplace.Application.Files.UseCases
{
    public class DeleteFiles : BaseUseCase<ICollection<Guid>, bool>
    {
        private readonly DeleteFile _deleteFile; 

        public DeleteFiles(DeleteFile deleteFile) {
            _deleteFile = deleteFile; 
        }

        public async Task<bool> Execute(ICollection<Guid> fileIds)
        {
            foreach (var fileId in fileIds)
            {
                var ok = await _deleteFile.Execute(fileId);
                if (!ok)
                {
                    return false; 
                }
            }

            return true; 
        }
    }
}
