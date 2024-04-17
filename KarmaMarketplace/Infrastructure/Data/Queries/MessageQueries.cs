using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Messging.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.Data.Queries
{
    public static class MessageQueries
    {
        public static IQueryable<MessageEntity> IncludeStandard(this IQueryable<MessageEntity> query)
        {
            return query
                .Include(x => x.Purchase)
                    .ThenInclude(y => y.Product)
                .Include(x => x.FromUser)
                .Include(x => x.Image)
                .Include(x => x.Review);
        }
    }
}
