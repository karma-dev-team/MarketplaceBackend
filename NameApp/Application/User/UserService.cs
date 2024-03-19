using NameApp.Application.Common.Interfaces;
using NameApp.Application.User.Interactors;
using NameApp.Application.User.Interfaces;
using NameApp.Domain.AccessService.Services;
using NameApp.Domain.User.Services;

namespace NameApp.Application.User
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext Context;
        private readonly UserEntityService ServiceEntity;
        private readonly PermissionEntityService PermissionService; 

        public UserService(
            IApplicationDbContext dbContext,
            UserEntityService userEntityService,
            PermissionEntityService permissionService
        ) {
            PermissionService = permissionService;
            ServiceEntity = userEntityService;
            Context = dbContext;
        }

        public RegisterInteractor RegisterInteractor()
        {
            return new RegisterInteractor(
                dbContext: Context,
                userEntityService: ServiceEntity, 
                permissionService: PermissionService
            ); 
        }
    }
}
