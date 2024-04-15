using KarmaMarketplace.Domain.User.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaMarketplace.Domain.Messging.Entities
{
    public class ChatReadRecord : BaseAuditableEntity
    {
        [Required]
        [ForeignKey(nameof(UserEntity))]
        public Guid ReadById { get; set; }
        [Required]
        [ForeignKey(nameof(ChatEntity))]
        public Guid ChatId { get; set; }
        [Required]
        public DateTime ReadAt {  get; set; }

        public ChatReadRecord(Guid readById, Guid chatId) {
            ReadById = readById;
            ChatId = chatId;
            ReadAt = DateTime.UtcNow;
        }
    }
}
