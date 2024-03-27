using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using KarmaMarketplace.Domain.Market.ValueObjects;
using KarmaMarketplace.Infrastructure;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class GetProduct : BaseUseCase<GetProductDto, ProductEntity?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user; 

        public GetProduct(IApplicationDbContext dbContext, IUser user)
        {
            _context = dbContext;
            _user = user;
        }

        public async Task<ProductEntity?> Execute(GetProductDto dto)
        {
            var product = await _context.Products
                .IncludeStandard()
                .Where(x => x.Id == dto.ProductId)
                .FirstOrDefaultAsync();

            var byUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == _user.Id);

            Guard.Against.Null(byUser, message: "Unauthorized"); 

            if (product == null)
            {
                return null;
            }

            product.RegisterView(byUser); 

            _context.Products.Update(product);
            await _context.SaveChangesAsync(); 

            return product; 
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

    public class GetAnalyticsInformation : BaseUseCase<GetAnalyticsDto, AnalyticsInformationDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user; 

        public GetAnalyticsInformation(IApplicationDbContext dbContext, IUser user) {
            _context = dbContext;
            _user = user; 
        } 

        public async Task<AnalyticsInformationDto> Execute(GetAnalyticsDto dto)
        {
            List<ProductEntity> products = []; 
            if (dto.ProductId != null)
            {
                var product = await _context.Products
                    .IncludeStandard()
                    .Where(x => x.Id == dto.ProductId)
                    .FirstOrDefaultAsync();

                Guard.Against.Null(product, message: "Product does not exists"); 

                products.Add(product);
            }
            else
            {
                var foundProducts = await _context.Products
                    .IncludeStandard()
                    .Where(x => x.CreatedBy.Id == _user.Id)
                    .Where(x => x.CreatedAt >= dto.StartDate && x.CreatedAt <= dto.EndDate)
                    .ToListAsync();
                products.AddRange(foundProducts);
            }

            int totalViews = 0;
            var viewsInWeek = 0;
            foreach ( var product in products)
            {
                totalViews += product.CountViews(
                        startDate: DateTime.UtcNow.AddYears(-20),
                        endDate: DateTime.UtcNow);
                viewsInWeek += product.CountViews(
                        startDate: DateTime.UtcNow.AddDays(-7),
                        endDate: DateTime.UtcNow); 
            }

            return new AnalyticsInformationDto(
                totalViews: totalViews, 
                viewsInWeek: viewsInWeek, 
                revenue: new(0)); 
        }
    }
}
