using KarmaMarketplace.Domain.Messging.Entities;

namespace KarmaMarketplace.Infrastructure.Caching
{
    public interface IMessagesCache
    {
        Task<ICollection<MessageEntity>> GetNewMessages(Guid userId);
        Task AddMessage(Guid userId, MessageEntity message);
    }
}
