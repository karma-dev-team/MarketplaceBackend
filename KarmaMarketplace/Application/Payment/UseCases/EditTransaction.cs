using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class EditTransaction : BaseUseCase<EditTransactionDto, TransactionEntity>
    {
        public EditTransaction() { }

        public async Task<TransactionEntity> Execute(EditTransactionDto dto)
        {
            return new();
        }
    }
}
