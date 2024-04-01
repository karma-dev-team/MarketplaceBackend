using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Market.Events
{
    public class AutoAnswerIsUsed(AutoAnswerEntity answer) : BaseEvent
    {
        public AutoAnswerEntity AutoAnswersEntity { get; set; } = answer;
    }

    public class AutoAnswerCreated(AutoAnswerEntity answer) : BaseEvent {
        public AutoAnswerEntity AutoAnswersEntity { get; set; } = answer;
    }
}
