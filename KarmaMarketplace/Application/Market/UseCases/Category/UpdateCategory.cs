using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Application.Market.Utils;
using KarmaMarketplace.Domain.Common;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Events;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.Interactors.Category
{
    public class UpdateCategory : BaseUseCase<UpdateCategoryDto, CategoryEntity>
    {
        private IApplicationDbContext _context;
        private IAccessPolicy _accessPolicy;
        private IEventDispatcher _eventDispatcher;

        public UpdateCategory(
            IApplicationDbContext dbContext, 
            IAccessPolicy accessPolicy, 
            IEventDispatcher eventDispatcher)
        {
            _context = dbContext;
            _accessPolicy = accessPolicy;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<CategoryEntity> Execute(UpdateCategoryDto dto)
        {
            await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Admin); 

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == dto.CategoryId);

            Guard.Against.Null(category, message: "Category does not exists");
            var oldCategory = category;

            if (!string.IsNullOrEmpty(dto.Name))
                category.Name = dto.Name;

            if (dto.Options.Count > 0)
            {
                var options = OptionFactory.CreateOptions(dto.Options);

                category.Options = options; 
            }

            _eventDispatcher.Dispatch(new CategoryUpdated(category, oldCategory)); 

            return category;
        }
    }
}
