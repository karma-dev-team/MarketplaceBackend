using KarmaMarketplace.Application.Market.EventHandlers;
using KarmaMarketplace.Application.Market.Interactors.Category;
using KarmaMarketplace.Application.Market.Interactors.Game;
using KarmaMarketplace.Application.Market.Interfaces;
using KarmaMarketplace.Application.Market.Services;
using KarmaMarketplace.Application.Market.UseCases.Game;
using KarmaMarketplace.Application.Market.UseCases.Product;
using KarmaMarketplace.Application.Market.UseCases.Review;

namespace KarmaMarketplace.Application.Market
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMarketApplicationServices(this IServiceCollection services)
        {
            // Products 
            services.AddScoped<CreateProduct>(); 
            services.AddScoped<UpdateProduct>();
            services.AddScoped<GetProduct>(); 
            services.AddScoped<GetProductsList>();
            services.AddScoped<DeleteProduct>();
            services.AddScoped<GetAnalyticsInformation>(); 
            
            // Games 
            services.AddScoped<CreateGame>(); 
            services.AddScoped<UpdateGame>();   
            services.AddScoped<GetGame>();  
            services.AddScoped<DeleteGame>();
            services.AddScoped<GetGamesList>();
            services.AddScoped<CountGames>(); 

            // Categories 
            services.AddScoped<CreateCategory>();
            services.AddScoped<UpdateCategory>();
            services.AddScoped<UpdateCategory>();
            services.AddScoped<DeleteCategory>(); 
            services.AddScoped<GetCategoriesList>();
            services.AddScoped<GetCategory>();

            // Reviews
            services.AddScoped<CreateReview>();
            services.AddScoped<GetReviewsList>();

            // Events
            services.AddScoped<ProductCreatedHandler>(); 

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IReviewService, ReviewService>();

            return services;
        }
    }
}
