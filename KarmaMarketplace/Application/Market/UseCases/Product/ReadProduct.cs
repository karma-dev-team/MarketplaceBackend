using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Application.Market.Services;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Enums;
using KarmaMarketplace.Domain.Market.Services;
using KarmaMarketplace.Infrastructure;
using KarmaMarketplace.Infrastructure.Data.Extensions;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class GetProduct : BaseUseCase<GetProductDto, ProductEntity?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly ProductDomainService _productService; 

        public GetProduct(IApplicationDbContext dbContext, IUser user, ProductDomainService productService)
        {
            _productService = productService; 
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

            if (byUser == null)
                return product; 

            if (product == null)
                return null;

            var view = product.CreateView(byUser);

            product.ProductViewed = _productService.CountViews(product);

            _context.ProductViews.Add(view); 
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
                .AsNoTracking()
                .IncludeStandard()
                .AsQueryable();

            if (dto.Name != null)
            {
                query = query.Where(x => x.Name == dto.Name);
            }

            if (dto.Status != null)
            {
                var status = (ProductStatus)dto.Status; 
                if (await _accessPolicy.CanAccess(Domain.User.Enums.UserRoles.Moderator))
                {
                    query = query.Where(x => x.Status == status);
                }
                else
                {
                    query = query.Where(x => x.Status == ProductStatus.Approved);
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

            if (dto.UserId != null)
            {
                query = query.Where(x => x.CreatedBy.Id == dto.UserId); 
            }

            query = query.Paginate(dto.Start, dto.Ends); 

            var result = await query.ToListAsync();
        
            return result;
        }
    }

    public class GetAnalyticsInformation : BaseUseCase<GetAnalyticsDto, AnalyticsInformationDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly ProductDomainService _productService; 

        public GetAnalyticsInformation(IApplicationDbContext dbContext, IUser user, ProductDomainService productService) {
            _context = dbContext;
            _productService = productService;   
            _user = user; 
        } 

        public async Task<AnalyticsInformationDto> Execute(GetAnalyticsDto dto)
        {
            List<ProductEntity> products = []; 
            if (dto.ProductId != null)
            {
                var product = await _context.Products
                    .IncludeStandard()
                    .AsNoTracking()
                    .Where(x => x.Id == dto.ProductId)
                    .FirstOrDefaultAsync();

                Guard.Against.Null(product, message: "Product does not exists"); 

                products.Add(product);
            }
            else
            {
                var foundProducts = await _context.Products
                    .AsNoTracking()
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
                totalViews += _productService.CountViews(
                        startDate: DateTime.UtcNow.AddYears(-20),
                        endDate: DateTime.UtcNow, 
                        product: product);
                viewsInWeek += _productService.CountViews(
                        startDate: DateTime.UtcNow.AddDays(-7),
                        endDate: DateTime.UtcNow, 
                        product: product); 
            }

            return new AnalyticsInformationDto(
                totalViews: totalViews, 
                viewsInWeek: viewsInWeek, 
                revenue: new(0)); 
        }
    }
}
