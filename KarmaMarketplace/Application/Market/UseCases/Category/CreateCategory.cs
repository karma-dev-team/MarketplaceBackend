using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Market.Interactors.Category
{
    public class CreateCategory : BaseUseCase<CreateCategoryDto, CategoryEntity>
    {
        public CreateCategory() { }

        public async Task<CategoryEntity> Execute(CreateCategoryDto dto)
        {
            return;
        }
    }
}
