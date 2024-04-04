using KarmaMarketplace.Application.Common.Interactors;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class BalanceOperation : BaseUseCase<InputDTO, OutputDTO>
    {
        public BalanceOperation() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
