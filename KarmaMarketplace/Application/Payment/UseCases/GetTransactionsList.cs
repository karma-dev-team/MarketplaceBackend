using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Payment.Entities;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetTransactionsList : BaseUseCase<GetTransactionsListDto, ICollection<TransactionEntity>>
    {
        public GetTransactionsList() { }

        public async Task<ICollection<TransactionEntity>> Execute(GetTransactionsListDto dto)
        {
            return []; 
        }
    }
}
