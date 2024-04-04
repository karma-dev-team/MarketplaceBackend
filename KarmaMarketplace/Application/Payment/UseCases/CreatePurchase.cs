using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class CreatePurchase : BaseUseCase<CreatePurchaseDto, PurchaseEntity>
    {
        public CreatePurchase() { }

        public async Task<PurchaseEntity> Execute(CreatePurchaseDto dto)
        {
            return new();
        }
    }
}
