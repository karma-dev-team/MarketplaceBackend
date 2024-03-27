using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace KarmaMarketplace.Domain.Market.Events
{
    public class CategoryCreated(CategoryEntity category) : BaseEvent 
    {
        public CategoryEntity Category { get; set; } = category; 
    }

    public class CategoryDeleted(CategoryEntity category) : BaseEvent {
        public CategoryEntity Category { get; set; } = category; 
    }

    public class CategoryUpdated(CategoryEntity category, CategoryEntity oldCategory) : BaseEvent
    {
        public CategoryEntity Category { get; set;} = category;
        public CategoryEntity OldCategory { get; set; } = oldCategory;
}
