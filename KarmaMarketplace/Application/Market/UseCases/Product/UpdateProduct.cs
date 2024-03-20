using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class UpdateProduct : BaseUseCase<UpdateProductDto, ProductEntity>
    {
        public UpdateProduct() { }

        public async Task<ProductEntity> Execute(UpdateProductDto dto)
        {
            return;
        }
    }
}
