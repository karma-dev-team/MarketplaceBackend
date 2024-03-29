using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Messging.Events
{
    public class ChatRead(ChatEntity chat) : BaseEvent
    {
        private ChatEntity Chat { get; set; } = chat; 
    }
}
