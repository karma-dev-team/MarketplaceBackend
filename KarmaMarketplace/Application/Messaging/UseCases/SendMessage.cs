using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Messaging.UseCases
{
    public class SendMessage : BaseUseCase<SendMessageDto, MessageEntity>
    {
        private IApplicationDbContext _context;
        private IFileService _fileService; 

        public SendMessage(
            IApplicationDbContext context, 
            IFileService fileService ) {
            _context = context; 
            _fileService = fileService; 
        }

        public async Task<MessageEntity> Execute(SendMessageDto dto)
        {
            var chat = await _context.Chats
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.ChatId);

            Guard.Against.Null(chat, message: "Chat does not exists");

            var fromUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.FromUserId);

            Guard.Against.Null(fromUser, message: "User does not exists");

            MessageEntity message; 

            if (dto.Image != null)
            {
                var newImage = await _fileService.UploadImage().Execute(dto.Image); 

                message = MessageEntity.CreateWithImage(dto.ChatId, fromUser, newImage); 
            } else if (dto.PurchaseId != null) {
                var purchase = await _context.Purchases.FirstOrDefaultAsync(x => x.Id == dto.PurchaseId);

                Guard.Against.Null(purchase, message: "Purchase does not exists");

                message = MessageEntity.CreateWithPurchase(dto.ChatId, fromUser, purchase);      
            } else if (!string.IsNullOrEmpty(dto.Text))
            {
                message = MessageEntity.CreateText(dto.ChatId, fromUser, dto.Text);
            } else
            {
                throw new Exception("Validation exception"); 
            }

            if (chat.Type == Domain.Messaging.Enums.ChatTypes.Private)
            {
                if (fromUser.Role == Domain.User.Enums.UserRoles.Moderator)
                {
                    chat.Participants.Add(fromUser);
                }

                var someUser = chat.Participants.FirstOrDefault(x => x.Id == dto.FromUserId);
                Guard.Against.Null(someUser, message: "You are not in participants"); 
            }

            chat.Messages.Add(message);

            _context.Messages.Add(message); 
            _context.Chats.Add(chat);
            await _context.SaveChangesAsync();

            return message;
        } 
    }
}
