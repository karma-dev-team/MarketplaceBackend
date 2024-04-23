using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Staff.Events;
using KarmaMarketplace.Domain.User.Entities;
using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Staff.Entities
{
    public class TicketCommentEntity : BaseAuditableEntity
    {
        // does not have events 
        [Required]
        public UserEntity CreatedBy { get; set; } = null!;
        [Required]
        public string Text { get; set; } = null!;
        public Guid? ParentCommentId { get; set; }
        [Required]
        public bool IsDeleted { get; set; } = false;
        [Required]
        public Guid TicketId { get; set; }
        [Required]
        public ICollection<FileEntity> Files { get; set; } = []; 

        public static TicketCommentEntity Create(UserEntity byUser, string text, ICollection<FileEntity> files)
        {
            var newComment = new TicketCommentEntity() { 
                CreatedBy = byUser,
                Text = text, 
                Files = files
            };

            return newComment; 
        }

        public static TicketCommentEntity Create(UserEntity byUser, string text, ICollection<FileEntity> files, Guid parentCommentId)
        {
            var newComment = new TicketCommentEntity()
            {
                CreatedBy = byUser,
                Files = files,
                Text = text,
                ParentCommentId = parentCommentId, 
            };

            return newComment;
        }

        public void Delete() {
            IsDeleted = true; 
        }
    }
}
