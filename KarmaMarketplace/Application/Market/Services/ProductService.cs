using KarmaMarketplace.Application.Market.Interactors.Category;
using KarmaMarketplace.Application.Market.Interfaces;
using KarmaMarketplace.Application.Market.UseCases.Product;
using Microsoft.Win32;

namespace KarmaMarketplace.Application.Market.Services
{
    public class ProductService : IProductService
    {
        private readonly IServiceProvider ServiceProvider;

        public ProductService(
            IServiceProvider serviceProvider
        )
        {
            ServiceProvider = serviceProvider;
        }

        public CreateProduct CreateProduct()
        {
            return ServiceProvider.GetRequiredService<CreateProduct>();
        }
        public UpdateProduct UpdateProduct()
        {
            return ServiceProvider.GetRequiredService<UpdateProduct>();
        }
        public DeleteProduct DeleteProduct()
        {
            return ServiceProvider.GetRequiredService<DeleteProduct>();
        }
        public GetProduct GetProduct()
        {
            return ServiceProvider.GetRequiredService<GetProduct>();
        }
        public GetProductsList GetProductsList()
        {
            return ServiceProvider.GetRequiredService<GetProductsList>();
        }
        public GetAnalyticsInformation GetAnalyticsInformation()
        {
            return ServiceProvider.GetRequiredService<GetAnalyticsInformation>();
        }
    }
}
