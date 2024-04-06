using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Market.Events
{
    public class ReviewCreated(ReviewEntity review) : BaseEvent
    {
        public ReviewEntity Review { get; set; } = review; 
    }
}
