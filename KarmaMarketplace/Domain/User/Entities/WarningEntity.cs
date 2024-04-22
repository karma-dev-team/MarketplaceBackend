using System.ComponentModel.DataAnnotations.Schema;

namespace KarmaMarketplace.Domain.User.Entities
{
    public class WarningEntity : BaseAuditableEntity 
    {
        public string Reason { get; set; } = null!; 
        [ForeignKey(nameof(UserEntity))]
        public Guid ByUserId { get; set; }
    }
}
