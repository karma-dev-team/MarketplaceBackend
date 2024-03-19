namespace NameApp.Domain.AccessService.Entities
{
    public class PermissionEntity : BaseAuditableEntity
    {
        public string Code { get; set; } = null!; 
        public string Name { get; set; } = null!;
    }
}
