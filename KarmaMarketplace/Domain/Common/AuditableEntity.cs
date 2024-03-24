using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public DateTime? Created { get; set; }
        public Guid LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set;}
    }
}
