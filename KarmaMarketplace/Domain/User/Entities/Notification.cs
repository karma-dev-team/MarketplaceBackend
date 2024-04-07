using KarmaMarketplace.Domain.User.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace KarmaMarketplace.Domain.User.Entities
{
    public class NotificationEntity : BaseAuditableEntity
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Text { get; set; } = string.Empty;
        [ForeignKey(nameof(UserEntity))]
        public Guid? FromUserId { get; set; }
        [ForeignKey(nameof(UserEntity))]
        public Guid ToUserId { get; set; }
        public NotificationTypes Type { get; set; } = NotificationTypes.Other;
        [Column(TypeName = "jsonb")]
        public string? Data { get; set; } = string.Empty; 
        
        public static NotificationEntity CreateFromSystem(
            string Title, string Text, Guid toUserId, Dictionary<string, string> data)
        {
            var notification = new NotificationEntity();

            // does not emit events! 
            notification.Title = Title;
            notification.Text = Text;
            notification.FromUserId = toUserId;
            notification.Data = JsonSerializer.Serialize(data); 

            return notification;
        } 

        public static NotificationEntity CreateFromUser(
            string Title, string Text, Guid toUserId, Guid fromUserId, Dictionary<string, string> data)
        {
            var notification = new NotificationEntity();

            notification.Title = Title;
            notification.Text = Text;
            notification.FromUserId = fromUserId;
            notification.ToUserId = toUserId;
            notification.Data = JsonSerializer.Serialize(data);

            return notification;

        }
    }
}
