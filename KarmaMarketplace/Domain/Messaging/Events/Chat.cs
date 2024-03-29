using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Messging.Events
{
    public class ChatRead(ChatEntity chat) : BaseEvent
    {
        public ChatEntity Chat { get; set; } = chat;
    }

    public class ChatCreated(ChatEntity chat) : BaseEvent
    {
        public ChatEntity Chat { get; set; } = chat;
    }
}
