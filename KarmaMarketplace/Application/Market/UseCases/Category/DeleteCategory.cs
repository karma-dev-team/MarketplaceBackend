using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;
using KarmaMarketplace.Domain.Market.Events;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Market.Interactors.Category
{
    public class DeleteCategory : BaseUseCase<DeleteCategoryDto, CategoryEntity>
    {
        private IApplicationDbContext _context;
        private IAccessPolicy _accessPolicy;
        private IEventDispatcher _eventDispatcher; 

        public DeleteCategory(IApplicationDbContext dbContext, IAccessPolicy accessPolicy, IEventDispatcher eventDispatcher)
        {
            _context = dbContext;
            _eventDispatcher = eventDispatcher; 
            _accessPolicy = accessPolicy;
        }

        public async Task<CategoryEntity> Execute(DeleteCategoryDto dto)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == dto.CategoryId);

            await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Owner); 

            Guard.Against.Null(category, message: "Category does not exists");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(); 

            await _eventDispatcher.Dispatch(new CategoryDeleted(category), _context); 

            return category;
        }
    }
}
