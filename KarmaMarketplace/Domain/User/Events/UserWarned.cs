using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.User.Events
{
    public class UserWarned(UserEntity user, string reason) : BaseEvent
    {
        public UserEntity User { get; set; } = user;
        public string Reason { get; set; } = reason;
    }
}
