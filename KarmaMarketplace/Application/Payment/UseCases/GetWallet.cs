using KarmaMarketplace.Application.Common.Interactors;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetWallet : BaseUseCase<InputDTO, OutputDTO>
    {
        public BalanceOperation() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
