using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.Data.Queries
{
    public static class WalletQueries
    {
        public static IQueryable<WalletEntity> IncludeStandard(this IQueryable<WalletEntity> query)
        {
            return query.
                Include(x => x.User); 
        }
    }
}
