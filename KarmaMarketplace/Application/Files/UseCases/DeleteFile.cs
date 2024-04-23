using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Infrastructure.Adapters.FileStorage;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Files.UseCases
{
    public class DeleteFile : BaseUseCase<Guid, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileStorageAdapter _fileStorageAdapter;

        public DeleteFile(IApplicationDbContext dbContext, IFileStorageAdapter storageAdapter) {
            _context = dbContext;
            _fileStorageAdapter = storageAdapter;
        }

        public async Task<bool> Execute(Guid fileId)
        {
            var file = await _context.Files.FirstOrDefaultAsync(x => x.Id == fileId); 
            
            if (file == null)
            {
                return false; 
            }

            await _fileStorageAdapter.DeleteFileAsync(file.FilePath); 

            _context.Files.Remove(file);
            await _context.SaveChangesAsync();

            return true; 
        }
    }
}
