namespace KarmaMarketplace.Application.Payment
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPaymentApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<PaymentAdapterFactory>(); 

            return services;
        }
    }
}
