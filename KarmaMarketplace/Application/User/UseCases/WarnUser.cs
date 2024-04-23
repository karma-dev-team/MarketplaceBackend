using KarmaMarketplace.Application.Common.Exceptions;
using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.User.UseCases
{
    public class WarnUser : BaseUseCase<WarnUserDto, UserEntity>
    {
        private IApplicationDbContext Context { get; set; }
        private IAccessPolicy AccessPolicy { get; set; }

        public WarnUser(IApplicationDbContext dbContext,
            IAccessPolicy accessPolicy)
        {
            Context = dbContext;
            AccessPolicy = accessPolicy;
        }

        public async Task<UserEntity> Execute(WarnUserDto dto)
        {
            await AccessPolicy.FailIfNoAccess(Domain.User.Enums.UserRoles.Admin);
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);
            if (user == null)
                throw new EntityDoesNotExists(nameof(UserEntity), "");

            user.Warn(dto.Reason, await AccessPolicy.GetCurrentUser()); 
            await Context.SaveChangesAsync();

            return user;
        }
    }
}
