using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.Data.Queries
{
    public static class GameQueries
    {
        public static IQueryable<GameEntity> IncludeStandard(this IQueryable<GameEntity> query)
        {
            return query
                .Include(x => x.Categories)
                    .ThenInclude(x => x.Options)
                .Include(x => x.Banner)
                .Include(x => x.Logo); 
        }
    }
}
