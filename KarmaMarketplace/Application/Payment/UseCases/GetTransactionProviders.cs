using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.Payment.Entities;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetTransactionProviders : BaseUseCase<GetTransactionProvidersDto, ICollection<TransactionProviderEntity>>
    {
        public GetPurchases() { }

        public async Task<ICollection<PurchaseEntity>> Execute(GetPurchasesDto dto)
        {
            return;
        }
    }
}
