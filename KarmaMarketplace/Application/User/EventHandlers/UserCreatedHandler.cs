using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Payment.EventHandlers;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.User.Events;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.User.EventHandlers
{
    public class UserCreatedHandler : IEventSubscriber<UserCreated>
    {
        private ILogger _logger;

        public UserCreatedHandler(ILogger<UserCreatedHandler> logger)
        {
            _logger = logger;
        }

        public Task HandleEvent(UserCreated eventValue, IApplicationDbContext _context)
        {
            var supportChat = ChatEntity.CreateSupport(eventValue.User);

            var wallet = WalletEntity.Create(eventValue.User);

            _context.Wallets.Add(wallet);

            _context.Chats.Add(supportChat);

            return Task.CompletedTask;
        }
    }
}
