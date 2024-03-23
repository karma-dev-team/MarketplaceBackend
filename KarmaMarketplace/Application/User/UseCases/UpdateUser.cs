using KarmaMarketplace.Application.Common.Exceptions;
using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Services;
using KarmaMarketplace.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KarmaMarketplace.Application.User.Interactors
{
    public class UpdateUser : BaseUseCase<UpdateUserDto, UserEntity>
    {
        private UserDomainService UserService; 
        private IApplicationDbContext Context;

        public UpdateUser(UserDomainService userService, 
            IApplicationDbContext context) {
            UserService = userService;
            Context = context; 
        }

        public async Task<UserEntity> Execute(UpdateUserDto dto)
        {
            var byUser = await Context.Users.FirstOrDefaultAsync(x => x.Id == dto.ByUserId);
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);
            if (user == null || byUser == null) 
                throw new EntityDoesNotExists(nameof(UserEntity), "");

            Dictionary<string, object> value = Convertor.ConvertToDictionary(dto); 

            UserService.Update(byUser, user, value: value); 

            return new();
        }
    }
}
