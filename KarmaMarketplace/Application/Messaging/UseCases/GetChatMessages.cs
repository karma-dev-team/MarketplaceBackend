using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Infrastructure.Data.Extensions;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Messaging.UseCases
{
    public class GetChatMessages : BaseUseCase<GetChatMessagesDto, ICollection<MessageEntity>>
    {
        private IApplicationDbContext _context;

        public GetChatMessages(IApplicationDbContext dbContext) {
            _context = dbContext; 
        }

        public async Task<ICollection<MessageEntity>> Execute(GetChatMessagesDto dto)
        {
            var messages = await _context.Messages
                .IncludeStandard()
                .Where(x => x.ChatID == dto.ChatId)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(dto.Start)
                .AsNoTracking()
                .Take(dto.Ends)
                .ToListAsync();

            return messages; 
        }
    }
}
