using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetWallet : BaseUseCase<GetWalletDto, WalletEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy; 

        public GetWallet(IApplicationDbContext dbContext, IAccessPolicy accessPolicy) {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<WalletEntity> Execute(GetWalletDto dto)
        {
            var wallet = await _context.Wallets
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.WalletId || x.UserId == dto.UserId);

            Guard.Against.Null(wallet, message: "Wallet does not exists");

            await _accessPolicy.FailIfNotSelfOrNoAccess(wallet.User.Id, Domain.User.Enums.UserRoles.Moderator); 
            
            return wallet;
        }
    }
}
