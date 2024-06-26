﻿using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Payment.Dto;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class SolveProblem : BaseUseCase<SolveProblemDto, bool>
    {
        public SolveProblem() { }

        public async Task<bool> Execute(SolveProblemDto dto)
        {
            return new();
        } 
    }
}
