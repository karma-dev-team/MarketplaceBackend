namespace KarmaMarketplace.Infrastructure.Adapters.Payment.Systems
{
    public class TestPaymentAdapter : IPaymentAdapter
    {
        public TestPaymentAdapter() { }

        public async Task<PaymentResult> InitPayment(PaymentPayload payload)
        {
            return new PaymentResult() { };
        }

        public async Task<PaymentStatus> CheckPaymentStatus(string paymentId)
        {
            return PaymentStatus.Completed;
        }

        public async Task<PaymentResult> RefundPayment(string paymentId, decimal amount)
        {
            return new PaymentResult() { };
        }
    }
}
