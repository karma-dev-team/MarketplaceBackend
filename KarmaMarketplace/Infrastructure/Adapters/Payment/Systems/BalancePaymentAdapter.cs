using KarmaMarketplace.Application.Common.Interfaces;

namespace KarmaMarketplace.Infrastructure.Adapters.Payment.Systems
{
    public class BalancePaymentAdapter : IPaymentAdapter
    {
        
        public BalancePaymentAdapter() { }

        public async Task<PaymentResult> InitPayment(PaymentPayload payload)
        {
            // омега костыль 
            var transactionId = payload.OrderId.Split(":")[1]; 

            return new PaymentResult
            {
                LinkUrl = $"/transaction/{transactionId}",
                PaymentId = "",
                Success = true,
            };
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
