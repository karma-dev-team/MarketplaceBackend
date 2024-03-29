using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Messaging.UseCases
{
    public class GetChat : BaseUseCase<GetChatDto, ChatEntity>
    {
        private IApplicationDbContext _context;
        private IUser _user; 

        public GetChat(
            IApplicationDbContext dbContext,
            IAccessPolicy accessPolicy,
            IUser user) {
            _context = dbContext;
            _user = user;
        }

        public async Task<ChatEntity> Execute(GetChatDto dto)
        {
            var chat = await _context.Chats
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.ChatId);

            Guard.Against.Null(chat, message: "Chat does not exists");

            var user = chat.Participants.FirstOrDefault(x => x.Id == _user.Id);

            Guard.Against.Null(user, message: "User not in participants");

            return chat; 
        }
    }
}
