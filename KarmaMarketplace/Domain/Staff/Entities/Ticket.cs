using KarmaMarketplace.Application.Market.Interactors.Category;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Staff.Enums;
using KarmaMarketplace.Domain.Staff.Events;

namespace KarmaMarketplace.Domain.Staff.Entities
{
    public class TicketEntity : BaseAuditableEntity
    {
        public string Text { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty; 
        public ICollection<ImageEntity> Images { get; set; } = new List<ImageEntity>();
        public TicketStatus Status { get; set; } = TicketStatus.Open; 

        public static TicketEntity Create(
            string text, string subject, ICollection<ImageEntity> images)
        {
            var ticket = new TicketEntity()
            {
                Text = text,
                Subject = subject,
                Images = images
            };

            ticket.AddDomainEvent(new TicketCreated(ticket)); 

            return ticket;
        }

        public void SetStatus(TicketStatus status) { 
            Status = status;
            AddDomainEvent(new TicketStatusUpdated(this, status)); 
        }
    }
}
