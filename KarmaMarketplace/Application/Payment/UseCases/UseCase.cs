using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class UseCase : BaseUseCase<InputDTO, OutputDTO>
    {
        public UseCase() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
