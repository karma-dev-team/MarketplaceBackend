using KarmaMarketplace.Application.Market.Interfaces;
using KarmaMarketplace.Application.Market.UseCases.Review;

namespace KarmaMarketplace.Application.Market.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IServiceProvider ServiceProvider;

        public ReviewService(
            IServiceProvider serviceProvider
        )
        {
            ServiceProvider = serviceProvider;
        }

        public CreateReview CreateReview() {
            return ServiceProvider.GetRequiredService<CreateReview>();
        }
        public GetReviewsList GetReviewsList()
        {
            return ServiceProvider.GetRequiredService<GetReviewsList>();
        }
    }
}
