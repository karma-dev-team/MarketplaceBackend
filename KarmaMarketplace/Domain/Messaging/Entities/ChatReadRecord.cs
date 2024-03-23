using KarmaMarketplace.Domain.User.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaMarketplace.Domain.Messging.Entities
{
    public class ChatReadRecord : BaseAuditableEntity
    {
        [ForeignKey(nameof(UserEntity))]
        public Guid ReadById { get; set; }
        [ForeignKey(nameof(ChatEntity))]
        public Guid ChatId { get; set; }
        public DateTime ReadAt {  get; set; }

        public ChatReadRecord(Guid readById, Guid chatId) {
            ReadById = readById;
            ChatId = chatId;
            ReadAt = DateTime.UtcNow;
        }
    }
}
