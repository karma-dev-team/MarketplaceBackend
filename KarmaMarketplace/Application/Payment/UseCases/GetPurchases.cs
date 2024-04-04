﻿using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetPurchases : BaseUseCase<GetPurchasesDto, ICollection<PurchaseEntity>>
    {
        public GetPurchases() { }

        public async Task<ICollection<PurchaseEntity>> Execute(GetPurchasesDto dto)
        {
            return new();
        }
    }
}
