using KarmaMarketplace.Application.Files.Dto;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Presentation.Web.Schemas
{
    public class SendMessageScheme
    {
        public string? Text { get; set; } = null!;
        [Required]
        public Guid ChatId { get; set; }
        public CreateFileDto? Image { get; set; }
        public Guid? PurchaseId { get; set; }
    }
}
