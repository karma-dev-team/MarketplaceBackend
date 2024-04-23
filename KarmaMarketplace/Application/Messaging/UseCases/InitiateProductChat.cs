using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Messaging.UseCases
{
    public class InitiateProductChat : BaseUseCase<InitiateProductChatDto, ChatEntity>
    {
        private IApplicationDbContext _context;
        private ILogger _logger; 

        public InitiateProductChat(
            IApplicationDbContext context, 
            ILogger<InitiateProductChat> logger)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ChatEntity> Execute(InitiateProductChatDto dto)
        {
            var fromUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == dto.FromUserId);
            Guard.Against.Null(fromUser, "User not found.");

            var product = await _context.Products
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.ProductId);
            Guard.Against.Null(product, "Product not found.");

            var toUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == product.CreatedBy.Id);
            Guard.Against.Null(toUser, "User not found.");

            var purchase = await _context.Purchases.FirstOrDefaultAsync(p => p.Transaction.Id  == dto.TransactionId);
            Guard.Against.Null(purchase, "Purchase not found.");

            _logger.LogInformation($"FromUser: {fromUser.Id}, toUser: {toUser.Id}");

            var existingChat = await _context.Chats
                .Include(x => x.Participants)
                .FirstOrDefaultAsync(x => x.Participants.Any(x => x.Id == fromUser.Id));

            if (existingChat != null)
            {
                _logger.LogInformation($"Does it exists, {existingChat.Id}");
                return existingChat; 
            }
            var chat = ChatEntity.CreatePrivate(
                name: product.CreatedBy.UserName,
                userParticipants: [fromUser, toUser],
                image: toUser.Image
            );

            _logger.LogInformation($"Added private chat, with participants: {chat.Participants}");
            _context.Users.UpdateRange([fromUser, toUser]);
            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();

            return chat; 
        }
    }
}
