namespace NameApp.Domain.AccessService.Entities
{
    public class GroupEntity : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<PermissionEntity> Permissions { get; set; } = []; 
    }
}
