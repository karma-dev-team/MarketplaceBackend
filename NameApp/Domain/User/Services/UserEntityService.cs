using NameApp.Domain.AccessService.Entities;
using NameApp.Domain.User.Entities;
using NameApp.Domain.User.Events;
using NameApp.Infrastructure.EventDispatcher;

namespace NameApp.Domain.User.Services
{
    public class UserEntityService
    {
        public IEventDispatcher EventDispatcher { get; set; }
        public PasswordService PasswordService { get; set; }

        public UserEntityService(IEventDispatcher eventDispatcher, PasswordService passwordService)
        {
            Guard.Against.Null(eventDispatcher, message: "Event dispatcher is not found.");

            EventDispatcher = eventDispatcher; 
            PasswordService = passwordService;
        }

        public UserEntity Create(
            string UserName, 
            string email, 
            string password, 
            PermissionEntity? permission 
        )
        {
            var user = new UserEntity()
            {
                Email = email, 
                UserName = UserName,
                Permission = permission
            };
            var hashedPassword = PasswordService.HashPassword(user, password);

            user.HashedPassword = hashedPassword;

            EventDispatcher.Dispatch(new UserCreated(User: user));

            return user; 
        }
    }
}
