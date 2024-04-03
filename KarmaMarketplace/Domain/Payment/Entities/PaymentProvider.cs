namespace KarmaMarketplace.Domain.Payment.Entities
{
    public class PaymentProvider : BaseAuditableEntity
    {
        
        public string Name { get; set; } = null!; 
        public List<PaymentSystem> Systems { get; set; } = new List<PaymentSystem>();
    }
}
