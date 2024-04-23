using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KarmaMarketplace.Application.Files.Dto
{
    public class CreateFileDto
    {
        public string? DownloadUrl { get; set; } = null!;
        public Guid? FileId { get; set; } = null!; 
        public string? Name { get; set; } = null!;
        public string? MimeType { get; set; } = "images/jpeg"; 
        public Stream? Stream { get; set; } = null!;
    }

    public class DeleteFileDto
    {
        // DONT REMOVE JSON IGNORE, AND DONT USE THIS SCHEME IN CONTROLLERS DIRECTLY CREATE SCHEMAS
        [JsonIgnore]
        public bool Directly { get; set; } = false; 
        public Guid FileId { get; set; }
    }
}
