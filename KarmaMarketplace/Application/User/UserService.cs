using KarmaMarketplace.Application.User.Interactors;
using KarmaMarketplace.Application.User.Interfaces;

namespace KarmaMarketplace.Application.User
{
    public class UserService : IUserService
    {
        private readonly IServiceProvider ServiceProvider;

        public UserService(
            IServiceProvider serviceProvider
        ) {
            ServiceProvider = serviceProvider; 
        }

        public CreateUser Register()
        {
            return ServiceProvider.GetRequiredService<CreateUser>(); 
        }

        public UpdateUser Update()
        {
            return ServiceProvider.GetRequiredService<UpdateUser>(); 
        }

        public DeleteUser Delete()
        {
            return ServiceProvider.GetRequiredService<DeleteUser>();
        }
        public GetUser Get()
        {
            return ServiceProvider.GetRequiredService<GetUser>();   
        }
        public GetUsersList GetList()
        {
            return ServiceProvider.GetRequiredService<GetUsersList>(); 
        }
    }
}
