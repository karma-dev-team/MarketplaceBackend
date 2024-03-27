using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
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

            List<OptionEntity> Options = []; 

            foreach (var option in dto.Options)
            {
                if (option.Type == Domain.Market.Enums.OptionTypes.Selector) {
                    Guard.Against.Null(option.Group, message: "Group for option is not provided"); 

                    Options.Add(OptionEntity.CreateSelector(
                        value: option.Value,
                        label: option.Label,
                        group: option.Group,
                        field: option.Field,
                        sequence: option.Sequence)); 
                } 
                if (option.Type == Domain.Market.Enums.OptionTypes.Switch)
                {
                    Options.Add(OptionEntity.CreateSwitch(
                        label: option.Label,
                        field: option.Field,
                        sequence: option.Sequence)); 
                }
                if (option.Type == Domain.Market.Enums.OptionTypes.Range) {
                    Guard.Against.Null(option.ValueRangeMin, message: "Value range min is null");
                    Guard.Against.Null(option.ValueRangeMax, message: "Value range max is null");

                    Options.Add(OptionEntity.CreateRange(
                        label: option.Label,
                        value: option.Value,
                        field: option.Field,
                        sequence: option.Sequence,
                        min: (int)option.ValueRangeMin,
                        max: (int)option.ValueRangeMax)); 
                }
            }

            var game = await _context.Games.FirstOrDefaultAsync(x => x.Id == dto.GameId);

            Guard.Against.Null(game, "Game does not exists"); 

            var newCategory = CategoryEntity.Create(
                name: dto.Name,
                options: Options, 
                gameId: dto.GameId, 
                slug: null); 

            _context.Categories.Add(newCategory); 
            await _context.SaveChangesAsync(); 

            return newCategory;
        }
    }
}
