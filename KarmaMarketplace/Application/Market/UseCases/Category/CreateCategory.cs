using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;

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

            List<OptionEntity> Options = []; 

            foreach (var option in dto.Options)
            {
                Options.Add(OptionEntity.Create(
                    ))
            }

            var newCategory = CategoryEntity.Create(
                name: dto.Name,
                options: ); 

            _context.Categories.Add(newCategory); 
            await _context.SaveChangesAsync(); 

            return;
        }
    }
}
