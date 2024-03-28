using KarmaMarketplace.Application.Market.Interactors.Category;
using KarmaMarketplace.Application.Market.Interactors.Game;
using KarmaMarketplace.Application.Market.Interfaces;

namespace KarmaMarketplace.Application.Market.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IServiceProvider ServiceProvider;

        public CategoryService(
            IServiceProvider serviceProvider
        )
        {
            ServiceProvider = serviceProvider;
        }

        public CreateCategory CreateCategory()
        {
            return ServiceProvider.GetRequiredService<CreateCategory>();
        }
        public UpdateCategory UpdateCategory()
        {
            return ServiceProvider.GetRequiredService<UpdateCategory>();
        }
        public DeleteCategory DeleteCategory()
        {
            return ServiceProvider.GetRequiredService<DeleteCategory>();
        }
        public GetCategory GetCategory()
        {
            return ServiceProvider.GetRequiredService<GetCategory>();
        }
        public GetCategoriesList GetCategoriesList()
        {
            return ServiceProvider.GetRequiredService<GetCategoriesList>();
        }
    }
}
