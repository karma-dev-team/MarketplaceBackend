﻿using KarmaMarketplace.Domain.User.Events;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Application.User.EventHandlers
{
    public class UserCreatedSubsciber : IEventSubscriber<UserCreated>
    {
        public async Task HandleEvent(UserCreated userCreated)
        {
            
        }
    }
}
