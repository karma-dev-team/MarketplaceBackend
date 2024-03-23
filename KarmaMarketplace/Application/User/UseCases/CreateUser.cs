using KarmaMarketplace.Application.User.Dto;
using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Exceptions;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Services;
using KarmaMarketplace.Application.Common.Exceptions;

namespace KarmaMarketplace.Application.User.Interactors
{
    public class CreateUser : BaseUseCase<CreateUserDto, UserEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserDomainService _userService;

        public CreateUser(
            IApplicationDbContext dbContext, 
            UserDomainService userEntityService)
        {
            _context = dbContext;
            _userService = userEntityService;
        }

        public async Task<UserEntity> Execute(CreateUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.EmailAddress); 
            if (user != null)
            {
                throw new EntityAlreadyExists(nameof(UserEntity), dto.EmailAddress, "EmailAddress"); 
            }

            var newUser = _userService.Create(
                UserName: dto.UserName, 
                email: dto.EmailAddress, 
                password: dto.Password
            ); 

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser; 
        }
    }
}
