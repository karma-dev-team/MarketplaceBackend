using KarmaMarketplace.Domain.Staff.Entities;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Staff.Events
{
    public class TicketAccepted(UserEntity byUser, TicketEntity ticket) : BaseEvent
    {
        public UserEntity ByUser { get; } = byUser;
        public TicketEntity Ticket { get; } = ticket;
    }
}
