using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.Data.Queries
{
    public static class UserQueries
    {
        public static IQueryable<UserEntity> IncludeStandard(this IQueryable<UserEntity> query)
        {
            return query
                .Include(x => x.Image); 
        }   
    }
}
