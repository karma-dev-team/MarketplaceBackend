using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Messaging.UseCases
{
    public class MarkChatAsRead : BaseUseCase<Guid, bool>
    {
        private IApplicationDbContext _context;
        private IUser _user; 

        public MarkChatAsRead(IApplicationDbContext dbContext, IUser user) {
            _context = dbContext; 
            _user = user;
        }

        public async Task<bool> Execute(Guid chatId)
        {
            var chat = await _context.Chats
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == chatId);

            Guard.Against.Null(chat, message: "Chat does not exists");
            Guard.Against.Null(_user.Id, message: "User does not exists"); 

            var readRecord = new ChatReadRecord((Guid)_user.Id, chatId); 

            chat.ReadMessages(readRecord);

            _context.ChatReads.Add(readRecord); 
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync(); 

            return true;
        }
    }
}
