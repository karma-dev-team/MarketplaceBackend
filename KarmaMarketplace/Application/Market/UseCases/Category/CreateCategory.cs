using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Application.Market.Utils;
using KarmaMarketplace.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.Interactors.Category
{
    public class CreateCategory : BaseUseCase<CreateCategoryDto, CategoryEntity>
    {
        private IApplicationDbContext _context;
        private IAccessPolicy _accessPolicy; 

        public CreateCategory(IApplicationDbContext dbContext, IAccessPolicy accessPolicy) {
            _context = dbContext; 
            _accessPolicy = accessPolicy;
        }

        public async Task<CategoryEntity> Execute(CreateCategoryDto dto)
        {
            await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Admin);

            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == dto.GameId);

            Guard.Against.Null(game, "Game does not exists"); 

            var newCategory = CategoryEntity.Create(
                name: dto.Name,
                options: OptionFactory.CreateOptions(dto.Options), 
                gameId: dto.GameId, 
                slug: null); 

            _context.Categories.Add(newCategory); 
            await _context.SaveChangesAsync(); 

            return newCategory;
        }
    }
}
