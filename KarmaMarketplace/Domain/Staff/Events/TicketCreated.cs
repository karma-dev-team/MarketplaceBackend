using KarmaMarketplace.Domain.Staff.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Staff.Events
{
    public class TicketCreated(TicketEntity ticket) : BaseEvent
    {
        public TicketEntity Ticket { get; set; } = ticket; 
    }
}
