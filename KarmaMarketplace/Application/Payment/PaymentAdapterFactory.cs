using KarmaMarketplace.Infrastructure.Adapters.Payment;

namespace KarmaMarketplace.Application.Payment
{
    // PaymentAdapterFactory.cs
    public class PaymentAdapterFactory
    {
        private readonly Dictionary<string, IPaymentAdapter> Adapters = new Dictionary<string, IPaymentAdapter>();

        public PaymentAdapterFactory()
        {
            // Регистрация доступных адаптеров
            Adapters.Add("PAYPAL", new PayPalychPaymentAdapter());
            // Добавьте другие адаптеры здесь
        }

        public IPaymentAdapter GetPaymentAdapter(string providerId)
        {
            if (Adapters.TryGetValue(providerId.ToUpper(), out var adapter))
            {
                return adapter;
            }

            throw new ArgumentException($"Payment adapter for provider ID {providerId} not found.", nameof(providerId));
        }
    }
}
