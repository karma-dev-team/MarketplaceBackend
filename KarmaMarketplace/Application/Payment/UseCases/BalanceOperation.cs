using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Payment.Dto;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class BalanceOperation : BaseUseCase<BalanceOperationDto, bool>
    {
        private IApplicationDbContext _context;
        private IAccessPolicy _accessPolicy;

        public BalanceOperation(IApplicationDbContext dbContext, IAccessPolicy accessPolicy) { 
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<bool> Execute(BalanceOperationDto dto)
        {
            await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Admin);

            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Id == dto.WalletId);

            Guard.Against.Null(wallet, message: "Wallet does not exists");

            wallet.AddBalance(dto.Balance); 

            return true;
        }
    }
}
