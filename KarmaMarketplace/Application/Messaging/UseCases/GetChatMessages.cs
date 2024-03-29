using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Domain.Messging.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Messaging.UseCases
{
    public class GetChatMessages : BaseUseCase<Guid, ICollection<MessageEntity>>
    {
        private IApplicationDbContext _context;

        public GetChatMessages(IApplicationDbContext dbContext) {
            _context = dbContext; 
        }

        public async Task<ICollection<MessageEntity>> Execute(Guid chatId)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(x => x.Id == chatId);

            Guard.Against.Null(chat, message: "Chat does not exists");
            return chat.Messages; 
        }
    }
}
