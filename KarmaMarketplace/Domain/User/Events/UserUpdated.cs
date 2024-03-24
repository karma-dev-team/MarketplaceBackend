using Microsoft.OpenApi.Any;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.User.Events
{
    public class UserUpdated(
        UserEntity user,
        UserEntity byUser
    ) : BaseEvent
    {
        public UserEntity User { get; } = user;
        public UserEntity ByUser { get; } = byUser; 
    }
}
