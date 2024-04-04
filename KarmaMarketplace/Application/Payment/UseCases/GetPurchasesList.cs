using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetPurchasesList : BaseUseCase<GetPurchasesListDto, ICollection<PurchaseEntity>>
    {
        public GetPurchasesList() { }

        public async Task<ICollection<PurchaseEntity>> Execute(GetPurchasesListDto dto)
        {
            return [];
        }
    }
}
