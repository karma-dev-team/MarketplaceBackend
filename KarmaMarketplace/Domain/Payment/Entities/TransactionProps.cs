namespace KarmaMarketplace.Domain.Payment.Entities
{
    public class TransactionPropsEntity : BaseAuditableEntity
    {
        public Guid? CreatorId { get; set; }
        public bool? PaidFromPendingIncome { get; set; }
        public string PaymentUrl { get; set; } = string.Empty;
        public string SuccessUrl { get; set; } = string.Empty;
        public string PaymentGateway { get; set; } = string.Empty;

        public static TransactionPropsEntity CreateGatewayProps(
            string paymentUrl,
            string successUrl,
            string paymentGateway)
        {
            var props = new TransactionPropsEntity
            {
                PaymentUrl = paymentUrl,
                SuccessUrl = successUrl,
                PaymentGateway = paymentGateway
            };

            return props;
        }
    }
}
