using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class ConfirmPurchase : BaseUseCase<ConfirmPurchaseDto, PurchaseEntity>
    {
        public ConfirmPurchase() { }

        public async Task<PurchaseEntity> Execute(ConfirmPurchaseDto dto)
        {
            return new();
        }
    }
}
