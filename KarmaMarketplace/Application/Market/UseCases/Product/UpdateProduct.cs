using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class UpdateProduct : BaseUseCase<UpdateProductDto, ProductEntity?>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;

        public UpdateProduct(IApplicationDbContext dbContext, IAccessPolicy accessPolicy)
        {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<ProductEntity> Execute(UpdateProductDto dto)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == dto.ProductId); 

            _accessPolicy.FailIfNotSelfOrNoAccess(); 

            return;
        }
    }
}
