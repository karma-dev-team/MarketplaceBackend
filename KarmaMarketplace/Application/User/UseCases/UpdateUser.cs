using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Domain.User.Entities;

namespace KarmaMarketplace.Application.User.Interactors
{
    public class UpdateUser : BaseUseCase<UpdateUserDTO, UserEntity>
    {
        public UpdateUser() { 
            
        }
    }
}
