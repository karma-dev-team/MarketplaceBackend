using KarmaMarketplace.Application.Payment.Interactors;
using KarmaMarketplace.Application.Payment.Interfaces;
using KarmaMarketplace.Application.Payment.Services;
using KarmaMarketplace.Application.Payment.UseCases;

namespace KarmaMarketplace.Application.Payment
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPaymentApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<PaymentAdapterFactory>();

            services.AddScoped<BalanceOperation>();
            services.AddScoped<ConfirmPurchase>();
            services.AddScoped<CreatePurchase>();
            services.AddScoped<CreateTransaction>(); 
            services.AddScoped<GetPurchasesList>();
            services.AddScoped<GetTransactionsList>();
            services.AddScoped<GetTransactionProviders>();
            services.AddScoped<GetWallet>();
            services.AddScoped<HandleTransaction>();
            services.AddScoped<SolveProblem>(); 
            services.AddScoped<UpdatePurchase>();
            services.AddScoped<UpdateTransaction>(); 
            services.AddScoped<HandleGatewayResult>();

            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IWalletService, WalletService>(); 

            return services;
        }
    }
}
