using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class EditPurchase : BaseUseCase<EditPurchaseDto, PurchaseEntity>
    {
        public EditPurchase() { }

        public async Task<PurchaseEntity> Execute(EditPurchaseDto dto)
        {
            return new();
        }
    }
}
