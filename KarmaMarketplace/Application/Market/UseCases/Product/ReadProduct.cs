using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class ReadProduct : BaseUseCase<ReadProductDto, ProductEntity>
    {
        public ReadProduct() { }

        public async Task<ProductEntity> Execute(UpdateProductDto dto)
        {
            return;
        }
    }
}
