using KarmaMarketplace.Application.Files.Dto;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Staff.Dto
{
    public class CreateTicketDto
    {
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; } = string.Empty;
        public ICollection<CreateFileDto> Files { get; set; } = []; 
    }
}
