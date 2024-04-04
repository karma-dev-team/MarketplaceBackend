using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Payment.Entities;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetTransactionProviders
        : BaseUseCase<GetTransactionProvidersDto, ICollection<TransactionProviderEntity>>
    {
        public GetTransactionProviders() { }

        public async Task<ICollection<TransactionProviderEntity>> Execute(
            GetTransactionProvidersDto dto)
        {
            return [];
        }
    }
}
