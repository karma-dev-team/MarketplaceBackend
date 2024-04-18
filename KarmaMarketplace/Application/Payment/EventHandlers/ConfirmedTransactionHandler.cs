using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Messaging.Dto;
using KarmaMarketplace.Application.Messaging.Interfaces;
using KarmaMarketplace.Application.Messaging.UseCases;
using KarmaMarketplace.Domain.Payment.Enums;
using KarmaMarketplace.Domain.Payment.Events;
using KarmaMarketplace.Infrastructure.Data.Queries;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Payment.EventHandlers
{
    public class ConfirmedTransactionHandler : IEventSubscriber<ConfirmedTransaction>
    {
        private readonly IMessagingService _messaging;
        private readonly ILogger _logger; 

        public ConfirmedTransactionHandler(
            IMessagingService messaging, 
            ILogger<ConfirmedTransactionHandler> logger) {
            _messaging = messaging;
            _logger = logger; 
        }

        public async Task HandleEvent(ConfirmedTransaction paymentEvent, IApplicationDbContext _context)
        {
            _logger.LogInformation("Trying to create chats for transaction"); 

            if (paymentEvent.Transaction.Direction == TransactionDirection.Out 
                    && paymentEvent.Transaction.Operation == TransactionOperations.Buy) {
                var purchase = await _context.Purchases
                    .IncludeStandard()
                    .FirstOrDefaultAsync(x => x.Transaction.Id == paymentEvent.Transaction.Id);

                Guard.Against.Null(purchase, message: "Purchase is not bound error");

                var chat = await _messaging.InitiateProductChat().Execute(new InitiateProductChatDto()
                {
                    FromUserId = paymentEvent.Transaction.CreatedByUser.Id, 
                    ProductId = purchase.Product.Id, 
                    TransactionId = paymentEvent.Transaction.Id,
                });

                _logger.LogInformation($"Chat: {chat.Id} has been created, by: {paymentEvent.Transaction.CreatedById}");

                await _messaging.SendMessage().Execute(new SendMessageDto()
                {
                    ChatId = chat.Id,
                    PurchaseId = purchase.Id,
                    FromUserId = paymentEvent.Transaction.CreatedByUser.Id,
                }); 

                var autoAnswer = await _context.AutoAnswers.FirstOrDefaultAsync(x => x.ProductId == purchase.Product.Id);                      

                if (autoAnswer != null)
                {
                    await _messaging.SendMessage().Execute(new SendMessageDto()
                    {
                        Text = autoAnswer.Answer,
                        FromUserId = purchase.Wallet.UserId,
                        ChatId = chat.Id,
                    });
                }

                purchase.SetBoundChat(chat.Id);
                _context.Purchases.Update(purchase);
            }
        }
    }
}
