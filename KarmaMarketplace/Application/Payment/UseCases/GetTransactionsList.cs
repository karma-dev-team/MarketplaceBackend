using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetTransactionsList : BaseUseCase<GetTransactionsListDto, ICollection<TransactionEntity>>
    {
        private IApplicationDbContext _context;
        private IUser _user;
        private IAccessPolicy _accessPolicy; 

        public GetTransactionsList(IApplicationDbContext dbContext, IUser user, IAccessPolicy accessPolicy) {
            _user = user; 
            _context = dbContext;
            _accessPolicy = accessPolicy;   
        }

        public async Task<ICollection<TransactionEntity>> Execute(GetTransactionsListDto dto)
        {
            var wallet = await _context.Wallets
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == dto.WalletId);

            Guard.Against.Null(wallet, message: "Wallet does not exists"); 

            await _accessPolicy.FailIfNotSelfOrNoAccess(
                wallet.UserId, Domain.User.Enums.UserRoles.Moderator);

            var result = await _context.Transactions
                .IncludeStandard()
                .AsNoTracking()
                .FilterByParams(
                    fromDate: dto.FromDate, 
                    toDate: dto.ToDate, 
                    operation: dto.Operation, 
                    providerName: dto.TransactionProvider)
                .ToListAsync(); 

            return result; 
        }
    }
}
