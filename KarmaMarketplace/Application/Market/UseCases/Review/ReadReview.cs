using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Market.UseCases.Review
{
    public class GetReview : BaseUseCase<InputDTO, OutputDTO>
    {
        public GetReview() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
