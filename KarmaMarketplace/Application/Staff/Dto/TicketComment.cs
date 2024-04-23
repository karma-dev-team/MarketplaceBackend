using KarmaMarketplace.Application.Files.Dto;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Staff.Dto
{
    public class CreateCommentDto
    {
        [Required]
        public string Text { get; set; } = null!; 
        [Required]
        public Guid TicketId { get; set; }
        public Guid? ParentCommentId { get; set; }
        public ICollection<CreateFileDto> Files { get; set; } = []; 
    }
}
