using KarmaMarketplace.Application.Common.Exceptions;
using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Infrastructure.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Application.User.Interactors
{
    public class DeleteUser : BaseUseCase<DeleteUserDto, UserEntity>
    {
        private IApplicationDbContext Context { get; set; }
        private IAccessPolicy AccessPolicy { get; set; }

        public DeleteUser(IApplicationDbContext dbContext, 
            IAccessPolicy accessPolicy) {
            Context = dbContext;
            AccessPolicy = accessPolicy;
        }

        public async Task<UserEntity> Execute(DeleteUserDto dto)
        {
            await AccessPolicy.FailIfNoAccess(dto.ByUserId, Domain.User.Enums.UserRoles.Admin); 
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);
            if (user == null)
                throw new EntityDoesNotExists(nameof(UserEntity), "");

            Context.Users.Remove(user);
            await Context.SaveChangesAsync();

            return user; 
        }
    }
}
