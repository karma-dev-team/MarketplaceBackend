using KarmaMarketplace.Application.Market.Interactors.Category;
using KarmaMarketplace.Application.Market.Interactors.Game;
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
            services.AddScoped<GetGamesList>();
            services.AddScoped<DeleteProduct>();
            
            // Games 
            services.AddScoped<CreateGame>(); 
            services.AddScoped<UpdateGame>();   
            services.AddScoped<GetGame>();  
            services.AddScoped<DeleteGame>();
            services.AddScoped<GetGamesList>();

            // Categories 
            services.AddScoped<CreateCategory>();
            services.AddScoped<UpdateCategory>();
            services.AddScoped<UpdateCategory>(); 
            services.AddScoped<GetCategoriesList>();
            services.AddScoped<GetCategory>();

            // Reviews
            services.AddScoped<CreateReview>();
            services.AddScoped<GetReviewsList>(); 

            return services;
        }
    }
}
