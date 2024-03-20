namespace KarmaMarketplace.Domain.Common
{
    public class BaseAuditableEntity
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public Guid LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set;}
    }
}
