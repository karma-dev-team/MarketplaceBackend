using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Domain.Messging.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Messaging.UseCases
{
    public class SendTextMessage : BaseUseCase<SendTextMessageDto, MessageEntity>
    {
        private IApplicationDbContext _context;
        private IAccessPolicy _accessPolicy;
        private IUser _user; 

        public SendTextMessage(
            IApplicationDbContext context, 
            IAccessPolicy accessPolicy, 
            IUser user) {
            _context = context; 
            _accessPolicy = accessPolicy;
            _user = user; 
        }

        public async Task<MessageEntity> Execute(SendTextMessageDto dto)
        {
            var chat = await _context.Chats
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x => x.Id == dto.ChatId);

            Guard.Against.Null(chat, message: "Chat does not exists");

            var fromUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == _user.Id);

            Guard.Against.Null(fromUser, message: "User does not exists");
            Guard.Against.Null(dto.Text, message: "Message text is not provided"); 

            var message = MessageEntity.CreateText(dto.ChatId, fromUser, dto.Text); 

            chat.Messages.Add(message);

            _context.Messages.Add(message); 
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return message;
        } 
    }
}
