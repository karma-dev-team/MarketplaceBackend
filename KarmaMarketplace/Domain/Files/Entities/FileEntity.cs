using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Files.Entities
{
    public class FileEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(256)]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required, MaxLength(128)]
        public string? MimeType { get; set; }

        // Assuming Size is meant to be a numeric type, not GUID.
        public long Size { get; set; } = 0;

        public FileEntity(Guid id, string fileName, string filePath, string? mimeType, long size)
        {
            Id = id;
            FileName = fileName;
            FilePath = filePath;
            MimeType = mimeType;
            Size = size;
        }
    }
}
