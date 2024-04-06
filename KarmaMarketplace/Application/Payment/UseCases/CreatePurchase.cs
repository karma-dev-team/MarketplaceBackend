using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.Payment.Dto;
using KarmaMarketplace.Domain.Market.ValueObjects;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.Payment.UseCases
{
    public class CreatePurchase : BaseUseCase<CreatePurchaseDto, PurchaseEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUser _user;
        private readonly PaymentAdapterFactory _paymentAdapterFactory;

        public CreatePurchase(
            IApplicationDbContext dbContext, 
            IUser user, 
            PaymentAdapterFactory adapterFactory) {
            _paymentAdapterFactory = adapterFactory; 
            _user = user;
            _context = dbContext;
        }

        public async Task<PurchaseEntity> Execute(CreatePurchaseDto dto)
        {
            var product = await _context.Products
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.ProductId);
            
            Guard.Against.Null(product, message: "Product does not exists"); 

            var provider = await _context.TransactionProviders.FirstOrDefaultAsync(x => x.Name == dto.ProviderName);

            Guard.Against.Null(provider, message: "Provider does not exists");

            var userWallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == _user.Id);

            var productOwnerWallet = await _context.Wallets.FirstOrDefaultAsync(x => x.UserId == product.CreatedBy.Id);
            Guard.Against.Null(userWallet, message: "User wallet does not exists!!");
            Guard.Against.Null(productOwnerWallet, message: "User wallet does not exists!!");


            var transaction = TransactionEntity.Create(
                value: product.CurrentPrice, 
                fee: new Money(0), 
                operation: Domain.Payment.Enums.TransactionOperations.Buy, 
                direction: Domain.Payment.Enums.TransactionDirection.Out, 
                provider: provider, 
                user: productOwnerWallet.User);

            var adapter = _paymentAdapterFactory.GetPaymentAdapter(providerId: provider.Name);

            if (adapter == null)
            {
                throw new NotImplementedException($"Implement {adapter} please"); 
            }

            var result = await adapter.InitPayment(new Infrastructure.Adapters.Payment.PaymentPayload()
            {
                Currency = nameof(transaction.Amount.Currency), 
                Amount = transaction.Amount.Amount + CalculateFee(product.CurrentPrice, provider.Fee), 
                AdditionalInfo = {}, 
                Custom = $"Оплата за {product.Name}", 
                Name = $"Оплата за {product.Name}", 
                OrderId = $"{product.Id}:{transaction.Id}", 
            });

            transaction.Props = TransactionPropsEntity.CreateGatewayProps(
                    result.LinkUrl,
                    $"/transaction/{transaction.Id}",
                    adapter.GetType().Name);

            var purchase = PurchaseEntity.Create(userWallet, product, transaction); 

            productOwnerWallet.FreezeAmount(transaction.Amount);
            userWallet.DecreaseBalance(transaction.Amount);

            _context.Purchases.Add(purchase);
            _context.Transactions.Add(transaction);
            _context.Wallets.UpdateRange([productOwnerWallet, userWallet]);
            await _context.SaveChangesAsync(); 

            return new();
        }

        private decimal CalculateFee(Money price, decimal fee)
        {
            return price.Amount / (100 / fee); 
        }
    }
}
