using KarmaMarketplace.Application.Common.Interactors;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.Interactors
{
    public class CreateTransaction : BaseUseCase<InputDTO, OutputDTO>
    {
        public BalanceOperation() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
