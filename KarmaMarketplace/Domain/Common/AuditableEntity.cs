using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set;}
    }
}
