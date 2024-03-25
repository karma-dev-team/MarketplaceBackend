using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class CreateProduct : BaseUseCase<CreateProductDto, ProductEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService; 

        public CreateProduct(IApplicationDbContext dbContext, IFileService fileService) {
            _context = dbContext;
            _fileService = fileService; 
        }

        public async Task<ProductEntity> Execute(CreateProductDto dto)
        {
            var byUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == dto.ByUserId);

            Guard.Against.Null(byUser, message: "correct user is not provided");

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == dto.CategoryId);

            Guard.Against.Null(category, message: "category does not exists");

            ICollection<ImageEntity> images = []; 
            foreach (var createImage in dto.Images)
            {
                var image = await _fileService.UploadImage().Execute(createImage); 
                Guard.Against.Null(image, message: $"Image does not exists, image: {createImage.Name}"); 
                images.Add(image);
            }

            var product = ProductEntity.Create(
                byUser: byUser,
                category: category,
                name: dto.Name,
                price: new Money(dto.Price),
                images: images,
                description: dto.Description,
                attributes: dto.Attributes);

            _context.Products.Add(product);
            await _context.SaveChangesAsync(); 

            return new(); 
        }
    }
}
