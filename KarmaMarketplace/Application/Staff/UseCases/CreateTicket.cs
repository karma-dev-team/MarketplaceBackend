using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Staff.Dto;
using KarmaMarketplace.Domain.Staff.Entities;

namespace KarmaMarketplace.Application.Staff.UseCases
{
    public class CreateTicket : BaseUseCase<CreateTicketDto, TicketEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;
        private readonly IFileService _fileService;

        public CreateTicket(IApplicationDbContext dbContext, IAccessPolicy accessPolicy, IFileService fileService)
        {
            _context = dbContext;
            _fileService = fileService;
            _accessPolicy = accessPolicy;
        }

        public async Task<TicketEntity> Execute(CreateTicketDto dto)
        {
            var byUser = await _accessPolicy.GetCurrentUser();

            var newFiles = await _fileService.UploadFiles().Execute(dto.Files);

            var ticket = TicketEntity.Create(dto.Text, dto.Subject, byUser, newFiles);

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return ticket; 
        }
    } 
}
