using KarmaMarketplace.Application.Market.Interactors.Category;

namespace KarmaMarketplace.Application.Market.Interfaces
{
    public interface ICategoryService
    {
        public CreateCategory CreateCategory(); 
        public UpdateCategory UpdateCategory();
        public DeleteCategory DeleteCategory();
        public GetCategory GetCategory();
        public GetCategoriesList GetCategoriesList(); 
    }
}
