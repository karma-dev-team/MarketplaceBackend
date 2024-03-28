using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;

namespace KarmaMarketplace.Application.Market.UseCases.Review
{
    public class CreateReview : BaseUseCase<CreateReviewDto, ReviewEntity>
    {
        public CreateReview() { }

        public async Task<ReviewEntity> Execute(CreateReviewDto dto)
        {
            return new();
        }
    }
}
