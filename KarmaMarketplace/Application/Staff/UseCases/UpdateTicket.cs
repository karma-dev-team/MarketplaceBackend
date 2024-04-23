using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Staff.Dto;
using KarmaMarketplace.Domain.Staff.Entities;
using KarmaMarketplace.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Staff.UseCases
{
    public class UpdateTicket : BaseUseCase<UpdateTicketDto, TicketEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;
        private readonly IFileService _fileService;

        public UpdateTicket(IApplicationDbContext dbContext, IAccessPolicy accessPolicy, IFileService fileService)
        {
            _context = dbContext;
            _fileService = fileService;
            _accessPolicy = accessPolicy;
        }

        public async Task<TicketEntity> Execute(UpdateTicketDto dto)
        {
            Guard.Against.Null(dto, nameof(dto));
            Guard.Against.Null(dto.TicketId, nameof(dto.TicketId));

            var ticket = await _context.Tickets
                .Include(x => x.Files)
                .FirstOrDefaultAsync(x => x.Id == dto.TicketId);

            Guard.Against.Null(ticket, $"Ticket with ID {dto.TicketId} does not exist.");

            var currentUser = await _accessPolicy.GetCurrentUser();

            // Ensure only the creator of the ticket can edit subject, text, and files
            if (ticket.CreatedBy.Id != currentUser.Id)
            {
                throw new AccessDenied("You are not authorized to edit this ticket.");
            }

            if (!string.IsNullOrEmpty(dto.Subject))
            {
                ticket.Subject = dto.Subject;
            }

            if (!string.IsNullOrEmpty(dto.Text))
            {
                ticket.Text = dto.Text;
            }

            if (dto.Files != null && dto.Files.Any())
            {
                var newFiles = await _fileService.UploadFiles().Execute(dto.Files);
                ticket.Files.AddRange(newFiles);
            }

            // Ensure only moderators can assign users
            if (await _accessPolicy.CanAccess(UserRoles.Moderator) && dto.AssignUserId != null)
            {
                var userToAssign = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.AssignUserId); 

                Guard.Against.Null(userToAssign, message: "User to assign does not exists"); 

                ticket.Accept(userToAssign);
            }

            await _context.SaveChangesAsync();

            return ticket;
        }
    }
}
