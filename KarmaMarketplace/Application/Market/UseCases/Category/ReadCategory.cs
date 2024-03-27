using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.Interactors.Category
{
    public class GetCategory : BaseUseCase<GetCategoryDto, CategoryEntity>
    {
        private readonly IApplicationDbContext _context;

        public GetCategory(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<CategoryEntity> Execute(GetCategoryDto dto)
        {
            var category = await _context.Categories
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.CategoryId);

            Guard.Against.Null(category, message: "Category does not exists"); 

            return category;
        }
    }

    public class GetCategoriesList : BaseUseCase<GetCategoriesListDto, ICollection<CategoryEntity>>
    {
        private readonly IApplicationDbContext _context; 

        public GetCategoriesList(IApplicationDbContext dbContext)
        {
            _context = dbContext; 
        }

        public async Task<ICollection<CategoryEntity>> Execute(GetCategoriesListDto dto)
        {
            var query = _context.Categories
                .IncludeStandard()
                .AsQueryable(); 
            
            if (!string.IsNullOrEmpty(dto.Name))
            {
                query = query.Where(x =>
                    EF.Functions
                        .ToTsVector(x.Name)
                        .Matches(EF.Functions.ToTsQuery(dto.Name))
                    ); 
            } 

            return await query.ToListAsync();
        }
    }
}
