using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.Data.Queries
{
    public static class PurchaseQueries
    {
        public static IQueryable<PurchaseEntity> IncludeStandard(this IQueryable<PurchaseEntity> query)
        {
            return query
                .Include(x => x.Transaction)
                .Include(x => x.Product)
                    .ThenInclude(x => x.CreatedBy)
                .Include(x => x.Chat)
                    .ThenInclude(x => x.Messages)
                .Include(x => x.Wallet); 
        }
    }
}
