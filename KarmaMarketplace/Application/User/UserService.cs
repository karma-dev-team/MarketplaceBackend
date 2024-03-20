using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Interactors;
using KarmaMarketplace.Application.User.Interfaces;
using KarmaMarketplace.Domain.User.Services;

namespace KarmaMarketplace.Application.User
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbContext Context;
        private readonly UserEntityService ServiceEntity;

        public UserService(
            IApplicationDbContext dbContext,
            UserEntityService userEntityService
        ) {
            ServiceEntity = userEntityService;
            Context = dbContext;
        }

        public CreateUser Register()
        {
            return new CreateUser(
                dbContext: Context,
                userEntityService: ServiceEntity
            ); 
        }
    }
}
