using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Domain.User.Events;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Application.Messaging.EventsHandlers
{
    public class UserCreatedHandler : IEventSubscriber<UserCreated>
    {
        private IApplicationDbContext _context; 

        public UserCreatedHandler(IApplicationDbContext dbContext) { 
            _context = dbContext;
        }

        public async Task HandleEvent(UserCreated eventValue)
        {
            var supportChat = ChatEntity.CreateSupport(eventValue.User);
            
            _context.Chats.Add(supportChat);
            await _context.SaveChangesAsync();
        }
    }
}
