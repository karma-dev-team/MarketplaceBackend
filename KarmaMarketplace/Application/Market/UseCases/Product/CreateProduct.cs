using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Files.Entities;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Payment.ValueObjects;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class CreateProduct : BaseUseCase<CreateProductDto, ProductEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IFileService _fileService;
        private readonly IAccessPolicy _accessPolicy;
        private readonly IUser _user; 

        public CreateProduct(IApplicationDbContext dbContext, IFileService fileService, IAccessPolicy accessPolicy, IUser user) {
            _context = dbContext;
            _fileService = fileService;
            _accessPolicy = accessPolicy;
            _user = user; 
        }

        public async Task<ProductEntity> Execute(CreateProductDto dto)
        {
            await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.User);

            var byUser = await _context.Users
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == _user.Id);

            Guard.Against.Null(byUser, message: "correct user is not provided");

            var category = await _context.Categories
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.CategoryId);

            Guard.Against.Null(category, message: "category does not exists");

            ICollection<FileEntity> images = []; 
            foreach (var createImage in dto.Images)
            {
                var image = await _fileService.UploadFile().Execute(createImage); 
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

            List<AutoAnswerEntity> autoAnswers = []; 

            foreach (var answer in dto.AutoAnswers)
            {
                autoAnswers.Add(
                    AutoAnswerEntity.Create(product.Id, answer)); 
            }

            _context.AutoAnswers.AddRange(autoAnswers);
            await _context.SaveChangesAsync(); 

            return product; 
        }
    }
}
