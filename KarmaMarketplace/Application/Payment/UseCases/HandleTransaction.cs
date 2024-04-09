using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Application.Payment.Exceptions;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.Payment.Enums;
using KarmaMarketplace.Infrastructure.Adapters.Payment;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
            var transaction = await _context.Transactions
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.TransactionId);

            Guard.Against.Null(transaction, message: "Transaction does not exists");
            var ok = Enum.TryParse(transaction.Provider.Name, out PaymentProviders provider);
            if (!ok)
                throw new ValidationException($"Transaction: {transaction.Id} does not have provider name"); 

            if (transaction.Status == TransactionStatusEnum.Pending && provider != PaymentProviders.Balance)
            {
                throw new PendingGatewayResult(transaction.Id); 
            }
            // to avoid implitcly dependency from TransactionQueries.IncludeDefault()
            var user = await _context.Users
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == transaction.CreatedByUser.Id);

            Guard.Against.Null(user, message: "What??");

            var userWallet = await _context.Wallets
                .IncludeStandard()
                .FirstAsync(x => x.User.Id == user.Id);

            Guard.Against.Null(userWallet, message: "User wallet");

            var purchase = await _context.Purchases
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Transaction.Id == transaction.Id);

            Guard.Against.Null(purchase, message: "Purchase is not bound to transaction"); 

            var productOwnerWallet = await _context.Wallets
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.User.Id == purchase.Product.CreatedBy.Id);

            Guard.Against.Null(productOwnerWallet, message: "Product owner wallet does not exists"); 

            productOwnerWallet.ConfirmTransaction(transaction, userWallet);

            _context.Wallets.Update(productOwnerWallet);
            _context.Wallets.Update(userWallet); 
            _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }
    }
}
