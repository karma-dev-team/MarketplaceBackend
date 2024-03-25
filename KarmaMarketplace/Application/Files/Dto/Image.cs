using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Files.Dto
{
    public class CreateFileDto
    {
        public string? DownloadUrl { get; set; } = null!; 
        public string? Name { get; set; } = null!;
        public string? MimeType { get; set; }
        public Stream? Stream { get; set; } = null!;
    }
}
