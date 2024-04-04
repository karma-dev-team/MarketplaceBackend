using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Payment.Dto;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class BalanceOperation : BaseUseCase<BalanceOperationDto, bool>
    {
        public BalanceOperation() { }

        public async Task<bool> Execute(BalanceOperationDto dto)
        {
            return new();
        }
    }
}
