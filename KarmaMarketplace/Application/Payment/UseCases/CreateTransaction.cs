using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.Interactors
{
    public class CreateTransaction : BaseUseCase<CreateTransactionDto, TransactionEntity>
    {
        public CreateTransaction() { }

        public async Task<TransactionEntity> Execute(CreateTransactionDto dto)
        {
            return new();
        }
    }
}
