using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaMarketplace.Domain.Messaging.Entities
{
    public class ChatParticipant
    {
        [ForeignKey(nameof(UserEntity)), Key]
        public Guid UserId { get; set; }
        [ForeignKey(nameof(ChatEntity)), Key]
        public Guid ChatId { get; set; }
    }
}
