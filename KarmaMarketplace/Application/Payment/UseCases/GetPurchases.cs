﻿using KarmaMarketplace.Application.Common.Interactors;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetPurchases : BaseUseCase<InputDTO, OutputDTO>
    {
        public BalanceOperation() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
