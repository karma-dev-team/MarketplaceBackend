using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Market.Interactors.Category
{
    public class DeleteCategory : BaseUseCase<Guid, CategoryEntity>
    {
        public DeleteCategory() { }

        public async Task<CategoryEntity> Execute(Guid dto)
        {
            return;
        }
    }
}
