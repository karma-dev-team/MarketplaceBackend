using KarmaMarketplace.Application.Market.UseCases.Product;

namespace KarmaMarketplace.Application.Market.Interfaces
{
    public interface IProductService
    {
        public CreateProduct CreateProduct();
        public UpdateProduct UpdateProduct();
        public DeleteProduct DeleteProduct();
        public GetProduct GetProduct();
        public GetCategoriesList GetCategoriesList();
        public RegisterView RegisterView();
        public GetAnalyticsInformtion GetAnalyticsInformation(); 
    }
}
