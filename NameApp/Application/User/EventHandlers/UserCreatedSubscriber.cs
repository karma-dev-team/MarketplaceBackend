using NameApp.Domain.User.Events;
using NameApp.Infrastructure.EventDispatcher;

namespace NameApp.Application.User.EventHandlers
{
    public class UserCreatedSubsciber : IEventSubscriber<UserCreated>
    {
        public void HandleEvent(UserCreated userCreated)
        {
            
        }
    }
}
