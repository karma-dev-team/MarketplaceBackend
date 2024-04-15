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
        private IApplicationDbContext _context;
        private ILogger _logger;

        public UserCreatedHandler(IApplicationDbContext dbContext, ILogger<UserCreatedHandler> logger)
        {
            _logger = logger;
            _context = dbContext;
        }

        public async Task HandleEvent(UserCreated eventValue)
        {
            var supportChat = ChatEntity.CreateSupport(eventValue.User);

            var existingWallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == eventValue.User.Id);

            var wallet = WalletEntity.Create(eventValue.User);

            _context.Wallets.Add(wallet);

            _context.Chats.Add(supportChat);
            await _context.SaveChangesAsync(); 
        }
    }
}
