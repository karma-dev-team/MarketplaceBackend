using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetAllTransactions : BaseUseCase<GetAllTransactionsDto, ICollection<TransactionEntity>>
    {
        public GetAllTransactions() { }

        public async Task<ICollection<TransactionEntity>> Execute(GetAllTransactionsDto dto)
        {
            return []; 
        }
    }
}
