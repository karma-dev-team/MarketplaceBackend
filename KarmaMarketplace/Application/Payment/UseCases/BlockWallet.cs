using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Payment.Dto;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class BlockWallet : BaseUseCase<BlockWalletDto, bool>
    {
        private IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy; 

        public BlockWallet(IApplicationDbContext dbContext, IAccessPolicy accessPolicy) { 
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<bool> Execute(BlockWalletDto dto)
        {
            await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Admin); 

            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Id == dto.WalletId);

            Guard.Against.Null(wallet, message: "Wallet does not exists"); 

            wallet.Block(dto.Reason);

            _context.Wallets.Add(wallet); 
            await _context.SaveChangesAsync(); 

            return true; 
        }
    }
}
