namespace KarmaMarketplace.Domain.Staff.Exceptions
{
    public class TicketIsClosed : Exception
    {
        public TicketIsClosed(Guid ticketId) : base($"Ticket is already closed, id: {ticketId}") { }
    }
}
