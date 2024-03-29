using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Messging.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.Data.Queries
{
    public static class ChatQueries
    {
        public static IQueryable<ChatEntity> IncludeStandard(this IQueryable<ChatEntity> query)
        {
            return query
                .Include(x => x.Participants)
                .Include(x => x.Messages)
                .Include(x => x.Owner)
                .Include(x => x.Photo)
                .Include(x => x.ReadRecords);
        }
    }
}
