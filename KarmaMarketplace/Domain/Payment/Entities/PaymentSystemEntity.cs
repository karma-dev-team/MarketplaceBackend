using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaMarketplace.Domain.Payment.Entities
{
    public class PaymentSystemEntity : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true; 
    }
}
