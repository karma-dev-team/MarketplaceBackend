using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Market.UseCases.Review
{
    public class CreteReview : BaseUseCase<InputDTO, OutputDTO>
    {
        public CreteReview() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
