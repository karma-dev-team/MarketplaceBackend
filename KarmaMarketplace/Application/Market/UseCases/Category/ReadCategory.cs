using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Market.Interactors.Category
{
    public class ReadCategory : BaseUseCase<ReadCategoryDto, CategoryEntity>
    {
        public ReadCategory() { }

        public async Task<CategoryEntity> Execute(ReadCategoryDto dto)
        {
            return;
        }
    }
}
