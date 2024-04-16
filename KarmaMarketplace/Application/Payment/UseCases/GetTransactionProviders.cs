using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class GetTransactionProviders
        : BaseUseCase<GetTransactionProvidersDto, ICollection<TransactionProviderEntity>>
    {
        private IApplicationDbContext _context;

        public GetTransactionProviders(IApplicationDbContext dbContext) {
            _context = dbContext;
        }

        public async Task<ICollection<TransactionProviderEntity>> Execute(
            GetTransactionProvidersDto dto)
        {
            return await _context.TransactionProviders
                .Include(x => x.Systems)
                .Include(x => x.Logo)
                .ToListAsync();
        }
    }
}
