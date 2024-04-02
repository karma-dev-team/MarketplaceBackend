namespace KarmaMarketplace.Domain.Payment.Entities
{
    public class PaymentSystem : BaseAuditableEntity
    {
        public string ProviderId { get; set; } // Строковый ID провайдера
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public PaymentProvider Provider { get; set; }
    }
}
