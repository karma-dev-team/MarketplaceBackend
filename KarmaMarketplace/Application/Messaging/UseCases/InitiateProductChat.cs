using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Messaging.UseCases
{
    public class InitiateProductChat : BaseUseCase<InitiateProductChatDto, ChatEntity>
    {
        private IApplicationDbContext _context;

        public InitiateProductChat(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ChatEntity> Execute(InitiateProductChatDto dto)
        {
            Guard.Against.Null(dto, nameof(dto));

            var fromUser = await _context.Users.FindAsync(dto.FromUserId);
            Guard.Against.Null(fromUser, "User not found.");

            var product = await _context.Products.FindAsync(dto.ProductId);
            Guard.Against.Null(product, "Product not found.");

            var toUser = await _context.Users.FindAsync(product.CreatedBy.Id);
            Guard.Against.Null(toUser, "User not found.");

            var purchase = await _context.Purchases.FirstOrDefaultAsync(p => p.Transaction.Id  == dto.TransactionId);
            Guard.Against.Null(purchase, "Purchase not found.");

            var chat = ChatEntity.CreatePrivate(
                name: product.Name,
                participants: new List<UserEntity> { fromUser, toUser },
                owner: toUser,
                image: toUser.Image
            );

            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();

            return chat; 
        }
    }
}
