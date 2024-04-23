using KarmaMarketplace.Domain.Messging.Entities;

namespace KarmaMarketplace.Infrastructure.Caching
{
    public class InMemoryMessageCache : IMessagesCache
    {
        private readonly Dictionary<Guid, ICollection<MessageEntity>> _messageDictionary = [];

        public Task<ICollection<MessageEntity>> GetNewMessages(Guid userId)
        {
            var messages = _messageDictionary
                .Where(x => x.Key == userId)
                .Select(x => x.Value)
                .FirstOrDefault(); 
            if (messages == null)
            {
                return Task.FromResult<ICollection<MessageEntity>>([]); 
            }
            _messageDictionary[userId].Clear();
            return Task.FromResult(messages);
        }

        public Task AddMessage(Guid userId, MessageEntity message)
        {
            if (!_messageDictionary.ContainsKey(userId))
            {
                _messageDictionary[userId] = []; 
            }
            _messageDictionary[userId].Add(message);
            return Task.CompletedTask;
        }
    }
}
