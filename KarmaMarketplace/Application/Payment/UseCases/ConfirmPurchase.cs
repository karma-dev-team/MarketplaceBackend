using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.Payment.Exceptions;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class ConfirmPurchase : BaseUseCase<ConfirmPurchaseDto, PurchaseEntity>
    {
        private IApplicationDbContext _context;
        private IUser _user;
        private ILogger _logger; 

        public ConfirmPurchase(IApplicationDbContext dbContext, IUser user, ILogger<ConfirmPurchase> logger) {
            _context = dbContext;
            _user = user; 
            _logger = logger;
        }

        public async Task<PurchaseEntity> Execute(ConfirmPurchaseDto dto)
        {
            var fromUser = await _context.Users
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == _user.Id);

            Guard.Against.Null(fromUser, message: "User does not exists");

            var product = await _context.Products
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.ProductId);
            Guard.Against.Null(product, message: "Product does not exists");

            var productOwner = product.CreatedBy; 
            var purchase = await _context.Purchases
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.PurchaseId);
            Guard.Against.Null(purchase, message: "Purchase does not exists");

            var productOwnerWallet = await _context.Wallets
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.UserId == productOwner.Id);

            Guard.Against.Null(productOwnerWallet, message: "Wallet does not exists"); 
            if (productOwnerWallet.Blocked)
            {
                throw new WalletIsBlocked(productOwnerWallet.Id); 
            }

            var review = ReviewEntity.Create(
                rating: dto.Rate, 
                text: dto.RateText, 
                createdBy: fromUser, 
                purchase: purchase, 
                product: product);

            Guard.Against.Null(
                purchase.ChatId, message: $"Purchase chat is not bound to purchase: purchaseId: {purchase.Id}"); 
            // to avoid implitlcly dependant from PurchaseQueries.IncludeStandard
            var chat = await _context.Chats
                .IncludeStandard() 
                .FirstOrDefaultAsync(x => x.Id == purchase.ChatId);

            Guard.Against.Null(chat, message: "Something is very bad");

            var message = chat.PurchaseMessage(purchase.Id);
            Guard.Against.Null(message, message: $"Purchase message does not exists, chatId: {chat.Id}"); 

            _context.Messages.Remove(message);

            var reviewMessage = MessageEntity.CreateWithReview(
                review: review, 
                purchase: purchase, 
                chatId: chat.Id, 
                fromUser: fromUser);
            chat.Messages.Add(reviewMessage);

            product.Sold(review);

            purchase.Complete(review);

            productOwnerWallet.UnfreezeAmount(purchase.Transaction.Amount);
            productOwnerWallet.IncreaseBalance(purchase.Transaction.Amount);

            _logger.LogInformation($"Product owner wallet: {productOwnerWallet.Id} " +
                                   $"had sold {product.Id} product"); 

            _context.Wallets.Update(productOwnerWallet); 
            _context.Products.Update(product); 
            _context.Chats.Update(chat);
            _context.Reviews.Add(review); 
            _context.Messages.Add(reviewMessage); 
            await _context.SaveChangesAsync(); 

            return new();
        }
    }
}
