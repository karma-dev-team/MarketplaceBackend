using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.User.Events
{
    public class UserCreated(UserEntity User) : BaseEvent
    {
        public UserEntity User { get; set; } = User; 
    }
}
