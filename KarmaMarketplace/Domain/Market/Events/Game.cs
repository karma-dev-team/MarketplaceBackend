using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Market.Events
{
    public class GameCreated(GameEntity game) : BaseEvent
    {
        public GameEntity Game { get; set; } = game; 
    }

    public class GameDeleted(GameEntity game, DateTime deletedAt) : BaseEvent
    {
        public GameEntity Game { get; set; } = game;
        public DateTime DeletedAt { get; set; } = deletedAt; 
    }
}
