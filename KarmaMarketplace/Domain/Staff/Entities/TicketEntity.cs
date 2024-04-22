using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Staff.Enums;
using KarmaMarketplace.Domain.Staff.Events;
using KarmaMarketplace.Domain.Staff.Exceptions;
using KarmaMarketplace.Domain.User.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaMarketplace.Domain.Staff.Entities
{
    public class TicketEntity : BaseAuditableEntity
    {
        [Required]
        public string Text { get; set; } = string.Empty;
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public ICollection<FileEntity> Files { get; set; } = [];
        [Required]
        public TicketStatus Status { get; private set; } = TicketStatus.Open;
        [Required]
        public UserEntity AssignedUser { get; private set; } = null!;
        [Required]
        public ICollection<TicketCommentEntity> Comments { get; private set; } = [];
        [ForeignKey(nameof(UserEntity))]
        public Guid? ClosedById { get; private set; }
        public UserEntity CreatedBy { get; private set; } = null!; 

        private void RaiseIfClosed()
        {
            if (Status == TicketStatus.Closed)
            {
                throw new TicketIsClosed(Id);
            }
        }

        public static TicketEntity Create(
            string text, string subject, UserEntity createdBy, ICollection<FileEntity> files)
        {
            var ticket = new TicketEntity()
            {
                CreatedBy = createdBy, 
                Text = text,
                Subject = subject,
                Files = files
            };

            ticket.AddDomainEvent(new TicketCreated(ticket)); 

            return ticket;
        }

        public void Accept(UserEntity byUser)
        {
            RaiseIfClosed();
            if (byUser.Id == AssignedUser.Id)
            {
                // do nothing 
                return; 
            }
            AssignedUser = byUser; 

            SetStatus(TicketStatus.InProgress); 

            AddDomainEvent(new TicketAccepted(byUser, this));
        }

        public void Close(UserEntity byUser)
        {
            RaiseIfClosed();

            ClosedById = byUser.Id;
            SetStatus(TicketStatus.Closed); 
        }

        private void SetStatus(TicketStatus status) { 
            Status = status;
            AddDomainEvent(new TicketStatusUpdated(this, status)); 
        }
 
        public void AddComment(UserEntity byUser, string text, ICollection<FileEntity> files, Guid? commentAnswerId = null)
        {
            RaiseIfClosed();

            TicketCommentEntity comment; 
            if (commentAnswerId == null)
            {
                comment = TicketCommentEntity.Create(byUser, text, files);
            }
            else
            {
                comment = TicketCommentEntity.Create(byUser, text, files, (Guid)commentAnswerId);
            }
            Comments.Add(comment);

            AddDomainEvent(new TicketCommentAdded(comment, this)); 
        }

        public void RemoveComment(Guid commentId)
        {
            RaiseIfClosed();
            var comment = Comments.Where(x => x.Id == commentId).FirstOrDefault(); 
            if (comment != null)
            {
                Comments.Remove(comment);

                AddDomainEvent(new TicketCommentRemoved(comment, this)); 
            }
        }
    }
}
