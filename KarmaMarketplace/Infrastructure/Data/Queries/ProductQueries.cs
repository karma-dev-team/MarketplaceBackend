using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.Data.Queries
{
    public static class ProductQueries
    {
        public static IQueryable<ProductEntity> IncludeStandard(this IQueryable<ProductEntity> query)
        {
            return query
                .Include(x => x.Category)
                .Include(x => x.BuyerUser)
                .Include(x => x.CreatedBy);
        }
    }
}
