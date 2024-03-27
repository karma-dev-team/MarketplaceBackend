using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.Data.Queries
{
    public static class CategoryQueries
    {
        public static IQueryable<CategoryEntity> IncludeStandard(this IQueryable<CategoryEntity> query)
        {
            return query
                .Include(x => x.Options); 
        }
    }
}
