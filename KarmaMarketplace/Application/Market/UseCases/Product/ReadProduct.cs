using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class GetProduct : BaseUseCase<GetProductDto, ProductEntity?>
    {
        private readonly IApplicationDbContext _context;

        public GetProduct(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<ProductEntity?> Execute(GetProductDto dto)
        {
            return await _context.Products
                .IncludeStandard()
                .Where(x => x.Id == dto.ProductId)
                .FirstOrDefaultAsync();
        }
    }

    public class GetProductsList : BaseUseCase<GetProductsListDto, ICollection<ProductEntity>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy; 

        public GetProductsList(
            IApplicationDbContext dbContext, 
            IAccessPolicy accessPolicy) {
            _context = dbContext;
            _accessPolicy = accessPolicy; 
        }
        
        public async Task<ICollection<ProductEntity>> Execute(GetProductsListDto dto)
        {
            var query = _context.Products
                .IncludeStandard()
                .AsQueryable();

            if (dto.Name != null)
            {
                query = query.Where(x => x.Name == dto.Name);
            }

            if (!string.IsNullOrEmpty(dto.Status))
            {
                Enum.TryParse(dto.Status, out ProductStatus status);
                List<ProductStatus> canSee = [ProductStatus.Approved, ProductStatus.Sold, ProductStatus.Approved]; 
                if (canSee.Contains(status))
                {
                    query = query.Where(x => x.Status == status);
                } else
                {

                }
            }

            if (dto.CategoryId != null)
            {
                query = query.Where(x => x.Category.Id == dto.CategoryId.Value);
            }

            if (dto.GameId != null)
            {
                query = query.Where(x => x.Category.GameID == dto.GameId.Value);
            }

            return await query.ToListAsync();
        }
    }

    public class GetAnalyticsInformation : BaseUseCase<UserActionDto, AnalyticsInformationDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;

        public GetAnalyticsInformation(IApplicationDbContext dbContext) {
            _context = dbContext; 
        } 

        public async Task<AnalyticsInformationDto> Execute(UserActionDto dto)
        {
            return new(); 
        }
    }
}
