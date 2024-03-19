using NameApp.Domain.AccessService.Entities;
using NameApp.Domain.AccessService.ValueObjects;

namespace NameApp.Domain.AccessService.Services
{
    public class PermissionEntityService
    {
        public PermissionEntityService() { }

        public PermissionEntity CreateUserPermission() { return new PermissionEntity(); }

        public PermissionEntity Create(string Name, PermissionCode code)
        {
            return new PermissionEntity()
            {
                Name = Name,
                Code = code.Code, 
            }; 
        }
    }
}
