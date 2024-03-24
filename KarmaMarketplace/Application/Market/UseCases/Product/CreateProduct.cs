using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class CreateProduct : BaseUseCase<CreateProductDto, ProductEntity>
    {
        private readonly IApplicationDbContext _context; 

        public CreateProduct(IApplicationDbContext dbContext) {
            _context = dbContext; 
        }

        public async Task<ProductEntity> Execute(CreateProductDto dto)
        {
            var byUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.ByUserId);

            Guard.Against.Null(byUser, message: "correct user is not provided");

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == dto.CategoryId);

            Guard.Against.Null(category, message: "category does not exists");

            var product = ProductEntity.Create(
                byUser: byUser,
                category: category,
                name: dto.Name,
                price: new Money(dto.Price),
                description: dto.Description,
                attributes: dto.Attributes);

            _context.Products.Add(product);
            await _context.SaveChangesAsync(); 

            return new(); 
        }
    }
}
