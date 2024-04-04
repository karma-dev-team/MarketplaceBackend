using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class UpdateTransaction : BaseUseCase<UpdateTransactionDto, TransactionEntity>
    {
        public UpdateTransaction() { }

        public async Task<TransactionEntity> Execute(UpdateTransactionDto dto)
        {
            return new();
        }
    }
}
