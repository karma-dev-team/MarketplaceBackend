using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.Payment.Enums;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.Data.Queries
{
    public static class TransactionQueries
    {
        public static IQueryable<TransactionEntity> IncludeStandard(this IQueryable<TransactionEntity> query)
        {
            return query
                .Include(x => x.CreatedBy)
                .Include(x => x.CreatedByUser); 
        }

        public static IQueryable<TransactionEntity> FilterByParams(this IQueryable<TransactionEntity> query, 
            DateTime? fromDate, DateTime? toDate, TransactionOperations? operation, string? providerName)
        {
            if (fromDate != null && toDate != null) {
                query = query
                    .Where(x => x.CreatedAt >= fromDate && x.CreatedAt <= toDate);
            }
            if (operation != null)
            {
                query = query.Where(x => x.Operation == operation); 
            }
            if (providerName != null)
            {
                query = query.Where(x => x.Provider.Name == providerName);
            }
            return query; 
        }
    }
}
