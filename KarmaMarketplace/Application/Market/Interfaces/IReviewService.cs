namespace KarmaMarketplace.Application.Market.Interfaces
{
    public interface IReviewService
    {
        public CreateReview CreateReview();
        public GetReview GetReview();
        public GetReviewsList GetReviewsList(); 
    }
}
