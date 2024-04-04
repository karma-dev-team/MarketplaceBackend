using KarmaMarketplace.Application.Common.Interactors;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetAllTransactions : BaseUseCase<InputDTO, OutputDTO>
    {
        public GetAllTransactions() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
