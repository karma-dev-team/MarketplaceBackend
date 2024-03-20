using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Common;

namespace KarmaMarketplace.Application.Market.Interactors.Category
{
    public class UpdateCategory : BaseUseCase<UpdateCategoryDto, CategoryEntity>
    {
        public UpdateCategory() { }

        public async Task<CategoryEntity> Execute(UpdateCategoryDto dto)
        {
            return;
        }
    }
}
