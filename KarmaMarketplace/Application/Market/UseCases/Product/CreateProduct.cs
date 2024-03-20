using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class CreateProduct : BaseUseCase<CreateProductDto, ProductEntity>
    {
        public CreateProduct() { }

        public async Task<ProductEntity> Execute(CreateProductDto dto)
        {
            return;
        }
    }
}
