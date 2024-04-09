using KarmaMarketplace.Domain.Staff.Entities;
using KarmaMarketplace.Domain.Staff.Enums;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Staff.Events
{
    public class TicketStatusUpdated(TicketEntity ticket, TicketStatus status) : BaseEvent
    {
        public TicketEntity Ticket { get; set; } = ticket;
        public TicketStatus Status { get; set; } = status; 
    }
}
