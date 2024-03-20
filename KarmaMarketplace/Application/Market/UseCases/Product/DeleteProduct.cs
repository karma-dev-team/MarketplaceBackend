using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Market.UseCases.Product
{
    public class DeleteProduct : BaseUseCase<DeleteProductDto, ProductEntity>
    {
        public DeleteProduct() { }

        public async Task<ProductEntity> Execute(DeleteProductDto dto)
        {
            return;
        }
    }
}
