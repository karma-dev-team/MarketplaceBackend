﻿using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.Data.Queries
{
    public static class ReviewQueries
    {
        public static IQueryable<ReviewEntity> IncludeStandard(this IQueryable<ReviewEntity> query)
        {
            return query
                .Include(x => x.Purchase)
                .Include(x => x.Product)
                .Include(x => x.CreatedBy)
                    .ThenInclude(x => x.Image); 
        }
    }
}
