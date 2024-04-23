using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.Staff.Entities;
using KarmaMarketplace.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Staff.UseCases
{
    public class DeleteTicket : BaseUseCase<Guid, TicketEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;

        public DeleteTicket(IApplicationDbContext dbContext, IAccessPolicy accessPolicy)
        {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<TicketEntity> Execute(Guid ticketId)
        {
            Guard.Against.Null(ticketId, nameof(ticketId));

            // Perform access check for admin role
            await _accessPolicy.FailIfNoAccess(UserRoles.Admin);

            var ticket = await _context.Tickets
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == ticketId);

            Guard.Against.Null(ticket, $"Ticket with ID {ticketId} does not exist.");

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return ticket; // Return the deleted ticket
        }
    }
}
