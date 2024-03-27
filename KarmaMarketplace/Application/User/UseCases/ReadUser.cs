using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.User.Interactors
{
    public class GetUsersList : BaseUseCase<GetListUserDto, List<UserEntity>>
    {
        private IApplicationDbContext _context;
        private IAccessPolicy _accessPolicy; 

        public GetUsersList(IApplicationDbContext dbContext, IAccessPolicy accessPolicy) {
            _context = dbContext; 
            _accessPolicy = accessPolicy;
        }

        public async Task<List<UserEntity>> Execute(GetListUserDto dto) {
            await _accessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Moderator);

            var result = await _context.Users
                .ToListAsync(); 

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
