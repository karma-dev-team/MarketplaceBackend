using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
        public static NotificationEntity CreateFromSystem(string Title, string Text, Guid toUserId)
        {
            var notification = new NotificationEntity();

            // does not emit events! 
            notification.Title = Title;
            notification.Text = Text;
            notification.FromUserId = toUserId;

            return notification;
        } 

        public static NotificationEntity CreateFromUser(string Title, string Text, Guid toUserId, Guid fromUserId)
        {
            var notification = new NotificationEntity();

            notification.Title = Title;
            notification.Text = Text;
            notification.FromUserId = fromUserId;
            notification.ToUserId = toUserId; 

            return notification;

        }
    }
}
