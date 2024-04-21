using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Domain.Market.Services
{
    public class ProductDomainService
    {
        private readonly IApplicationDbContext _dbContext;

        public ProductDomainService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CountViews(ProductEntity product, DateTime startDate, DateTime endDate)
        {
            return _dbContext.ProductViews
                             .Count(visit => visit.ProductId == product.Id &&
                                             visit.CreatedAt >= startDate &&
                                             visit.CreatedAt <= endDate);
        }

        public int CountViews(ProductEntity product)
        {
            return _dbContext.ProductViews
                             .Count(visit => visit.ProductId == product.Id);
        }
    }
}
