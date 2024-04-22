using KarmaMarketplace.Domain.Staff.Entities;

namespace KarmaMarketplace.Domain.Staff.Events
{
    public class TicketCommentAdded(TicketCommentEntity comment, TicketEntity ticket) : DomainEvent
    {
        public TicketEntity Ticket { get; set; } = ticket; 
        public TicketCommentEntity Comment { get; set; } = comment;
    }

    public class TicketCommentRemoved(TicketCommentEntity comment, TicketEntity ticket) : DomainEvent
    {
        public TicketEntity Ticket { get; set; } = ticket;
        public TicketCommentEntity Comment { get; set; } = comment;
    }
}
