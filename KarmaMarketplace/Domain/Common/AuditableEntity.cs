using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Common
{
    public class BaseAuditableEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? Created { get; set; }
        public Guid LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set;}
    }
}
