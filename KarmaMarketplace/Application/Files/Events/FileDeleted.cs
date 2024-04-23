using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Application.Files.Events
{
    public class FileDeleted(FileEntity file) : BaseEvent
    {
        public FileEntity File { get; set; } = file; 
    }
}
