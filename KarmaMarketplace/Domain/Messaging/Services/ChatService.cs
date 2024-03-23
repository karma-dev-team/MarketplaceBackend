using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Messging.Services
{
    public class ChatService
    {
        private IEventDispatcher EventDispatcher { get; set; }

        public ChatService(IEventDispatcher eventDispatcher) { 
            EventDispatcher = eventDispatcher;
        }

        public int ReadChat(ChatEntity chat)
        {
            
        }
    }
}
