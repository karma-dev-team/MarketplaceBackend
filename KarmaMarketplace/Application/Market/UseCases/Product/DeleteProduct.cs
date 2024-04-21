using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class DeleteProduct : BaseUseCase<DeleteProductDto, ProductEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;
        private readonly IUser _user; 

        public DeleteProduct(IApplicationDbContext dbContext, IAccessPolicy accessPolicy, IUser user) {
            _context = dbContext;
            _accessPolicy = accessPolicy;
            _user = user; 
        }

        public async Task<ProductEntity> Execute(DeleteProductDto dto)
        {
            var product = await _context.Products
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.ProductId);

            Guard.Against.Null(product, message: "product does not exists");
            Guard.Against.Null(_user.Id); 

            await _accessPolicy.FailIfNotSelfOrNoAccess(
                (Guid)_user.Id, Domain.User.Enums.UserRoles.Admin, product.CreatedBy.Id);

            await _context.ProductViews
                .Where(x => x.ProductId == product.Id)
                .ExecuteDeleteAsync();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync(); 

            return product;
        }
    }
}
