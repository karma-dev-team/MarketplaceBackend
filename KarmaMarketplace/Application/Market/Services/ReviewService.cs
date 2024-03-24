using KarmaMarketplace.Application.Market.Interfaces;
using KarmaMarketplace.Application.Market.UseCases.Review;

namespace KarmaMarketplace.Application.Market.Services
{
    public class ReviewService : IReviewService
    {
        public CreateReview CreateReview();
        public GetReview GetReview();
        public GetReviewsList GetReviewsList();
    }
}
