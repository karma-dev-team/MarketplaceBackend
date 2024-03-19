using NameApp.Domain.User.Entities;
using NameApp.Infrastructure.EventDispatcher;

namespace NameApp.Domain.User.Events
{
    public class UserCreated(UserEntity User) : BaseEvent
    {
        public UserEntity User { get; set; } = User; 
    }
}
