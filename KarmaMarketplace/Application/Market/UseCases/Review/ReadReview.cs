using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Market.Dto;
using KarmaMarketplace.Domain.Market.Entities;

namespace KarmaMarketplace.Application.Market.UseCases.Review
{
    public class GetReviewsList : BaseUseCase<GetReviewsListDto, ICollection<ReviewEntity>>
    {
        public GetReviewsList() { }

        public async Task<ICollection<ReviewEntity>> Execute(GetReviewsListDto dto)
        {
            return [];
        }
    }
}
