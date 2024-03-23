using KarmaMarketplace.Domain.Messging.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Payment.Entities;

namespace KarmaMarketplace.Domain.Messging.Entities
{
    public class MessageEntity : BaseAuditableEntity
    {
        [ForeignKey("ChatEntity")]
        public Guid ChatID { get; set; }

        public UserEntity FromUser { get; set; } = null!;

        public string Text { get; set; } = null!;

        public MessageTypes Type { get; set; } = MessageTypes.Text; 

        public ImageEntity Image { get; set; } = null!; 
        
        public ReviewEntity? Review { get; set; }

        public PurchaseEntity Purchase { get; set; } = null!;
    }
}
