using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class UpdatePurchase : BaseUseCase<UpdatePurchaseDto, PurchaseEntity>
    {
        public UpdatePurchase() { }

        public async Task<PurchaseEntity> Execute(UpdatePurchaseDto dto)
        {
            return new();
        }
    }
}
