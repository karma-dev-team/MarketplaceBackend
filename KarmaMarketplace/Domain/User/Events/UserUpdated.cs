using Microsoft.OpenApi.Any;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.User.Events
{
    public class UserUpdated(
        UserEntity user,
        UserEntity byUser,
        Dictionary<string, object> changedValues
    ) : BaseEvent
    {
        public UserEntity User { get; } = user;
        public Dictionary<string, object> ChangedValues { get; } = changedValues; 
        public UserEntity ByUser { get; } = byUser; 
    }
}
