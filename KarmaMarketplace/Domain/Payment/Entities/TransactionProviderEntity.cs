namespace KarmaMarketplace.Domain.Payment.Entities
{
    public class TransactionProviderEntity : BaseAuditableEntity
    {
        public string Name { get; set; } = null!; 
        public List<PaymentSystemEntity> Systems { get; set; } = new List<PaymentSystemEntity>();
    }
}
