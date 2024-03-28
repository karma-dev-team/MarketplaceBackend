using KarmaMarketplace.Domain.Messging.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Messging.Events
{
    public class MessageCreated(MessageEntity message) : BaseEvent
    {
        public MessageEntity Message { get; set; } = message; 
    }
}
