using KarmaMarketplace.Application.Market.UseCases.Review;

namespace KarmaMarketplace.Application.Market.Interfaces
{
    public interface IReviewService
    {
        public CreateReview CreateReview();
        public GetReviewsList GetReviewsList(); 
    }
}
