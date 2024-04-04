using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class HandleTransaction : BaseUseCase<HandleTransactionDto, TransactionEntity>
    {
        public HandleTransaction() { }

        public async Task<TransactionEntity> Execute(HandleTransactionDto dto)
        {
            return new();
        }
    }
}
