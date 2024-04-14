using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Messaging.Interfaces;
using KarmaMarketplace.Application.Payment.Interfaces;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.Payment.Events;
using KarmaMarketplace.Domain.User.Events;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Application.Payment.EventHandlers
{
    public class CreateWalletUserCreated : IEventSubscriber<UserCreated>
    {
        private readonly IApplicationDbContext _context;

        public CreateWalletUserCreated(
            IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task HandleEvent(UserCreated createdEvent)
        {
            var existingWallet = _context.Wallets.FirstOrDefault(x => x.UserId == createdEvent.User.Id); 

            if (existingWallet == null) { 
                var wallet = WalletEntity.Create(createdEvent.User);
            
                _context.Wallets.Add(wallet); 
                await _context.SaveChangesAsync();
            }
        }
    }
}
