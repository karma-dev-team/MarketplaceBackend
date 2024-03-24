using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class DeleteProduct : BaseUseCase<DeleteProductDto, ProductEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy; 

        public DeleteProduct(IApplicationDbContext dbContext, IAccessPolicy accessPolicy) {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<ProductEntity> Execute(DeleteProductDto dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == dto.ProductId);

            Guard.Against.Null(product, message: "product does not exists");

            await _accessPolicy.FailIfNotSelfOrNoAccess(
                dto.ByUserId, product.CreatedBy.Id, Domain.User.Enums.UserRoles.Admin); 

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(); 

            return product;
        }
    }
}
