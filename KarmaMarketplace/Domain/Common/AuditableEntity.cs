using System.ComponentModel.DataAnnotations;

namespace KarmaMarketplace.Domain.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedAt { get; set; }
        public Guid? LastModifiedById { get; set; }
        public DateTime? LastModifiedAt { get; set;}
    }
}
