using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Messaging.UseCases
{
    public class GetChatsList : BaseUseCase<GetChatsListDto, ICollection<ChatEntity>>
    {
        private IApplicationDbContext _context;
        private IUser _user;

        public GetChatsList(
            IApplicationDbContext context,
            IUser user) {
            _context = context;
            _user = user; 
        }

        public async Task<ICollection<ChatEntity>> Execute(GetChatsListDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);

            Guard.Against.Null(user, message: "User does not exists");

            var query = _context.Chats
                .IncludeStandard()
                .AsQueryable(); 

            if (dto.IsProblemChat)
            {
                query = query.Where(x => x.Type == Domain.Messaging.Enums.ChatTypes.Support); 
            }
            if (dto.UserId != null)
            {
                query = query.Where(
                    x => x.Participants.Any(participant => participant.Id == dto.UserId));
            }

            var result = await query.ToListAsync();

            return result; 
        }
    }
}
