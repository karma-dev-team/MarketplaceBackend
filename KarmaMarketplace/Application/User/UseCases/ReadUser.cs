using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.User.Entities;

namespace KarmaMarketplace.Application.User.Interactors
{
    public class GetUsersList : BaseUseCase<GetListUserDto, List<UserEntity>>
    {
        private IApplicationDbContext Context; 

        public GetUsersList(IApplicationDbContext context) {
            Context = context;
        }

        public async Task<List<UserEntity>> Execute(GetListUserDto dto) {
            return []; 
        }
    }

    public class GetUser : BaseUseCase<GetUserDto, UserEntity>
    {
        private IApplicationDbContext Context;

        public GetUser(IApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<UserEntity> Execute(GetUserDto dto)
        {
            return new();
        }
    }
}
