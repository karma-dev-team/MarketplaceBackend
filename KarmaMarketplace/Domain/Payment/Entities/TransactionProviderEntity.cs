using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Payment.Entities
{
    public class TransactionProviderEntity : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public decimal Fee { get; set; } = 0; 
        [Required]
        public List<PaymentSystemEntity> Systems { get; set; } = new List<PaymentSystemEntity>();
    }
}
