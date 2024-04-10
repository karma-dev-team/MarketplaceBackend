namespace KarmaMarketplace.Infrastructure.Data.Extensions
{
    public static class PagniationExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int start, int end)
        {
            query = query.Skip(start);
            if (end == 0)
            {
                return query; 
            }
            return query.Take(end);
        }
    }
}
