using KarmaMarketplace.Application.Market.Interactors.Category;
using KarmaMarketplace.Application.Market.Interfaces;

namespace KarmaMarketplace.Application.Market.Services
{
    public class CategoryService : ICategoryService
    {
        public CreateCategory CreateCategory();
        public UpdateCategory UpdateCategory();
        public DeleteCategory DeleteCategory();
        public GetCategory GetCategory();
        public GetCategoriesList GetCategoriesList();
        public GetCategoryProducts GetCategoryProducts();
    }
}
