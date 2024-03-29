using KarmaMarketplace.Application.Files.Dto;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Application.Messaging.Dto
{
    public class SendMessageDto 
    {
        public string? Text { get; set; } = null!;
        [Required]
        public Guid ChatId { get; set; }
        public CreateFileDto? Image { get; set; }
        public Guid? PurchaseId { get; set; }
    }

    public class EditMessageMediaDTO
    {
        [Required]
        public Guid MessageId { get; set; }
        [Required]
        public Guid ChatId { get; set; }
        [Required]
        public CreateFileDto Image { get; set; } = null!; 
    }

    public class EditMessageTextDTO
    {
        [Required]
        public Guid ChatId { get; set; }
        [Required]
        public Guid MessageId { get; set; }
        [Required]
        public string Text { get; set; } = null!; 
    }
}
