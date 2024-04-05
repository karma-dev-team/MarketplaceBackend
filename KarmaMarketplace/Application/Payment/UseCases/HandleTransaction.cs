using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Payment.Entities;
using Microsoft.AspNetCore.Components.Forms;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class HandleTransaction : BaseUseCase<HandleTransactionDto, TransactionEntity>
    {
        private IApplicationDbContext _context;

        public HandleTransaction(IApplicationDbContext dbContext) { 
            _context = dbContext;
        }

        public async Task<TransactionEntity> Execute(HandleTransactionDto dto)
        {


            return new();
        }
    }
}
