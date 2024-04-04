using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetWallet : BaseUseCase<GetWalletDto, WalletEntity>
    {
        public GetWallet() { }

        public async Task<WalletEntity> Execute(GetWalletDto dto)
        {
            return new();
        }
    }
}
