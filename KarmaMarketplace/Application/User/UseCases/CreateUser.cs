using KarmaMarketplace.Application.User.Dto;
using Microsoft.EntityFrameworkCore;
using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Application.Common.Exceptions;
using KarmaMarketplace.Infrastructure;

namespace KarmaMarketplace.Application.User.Interactors
{
    public class CreateUser : BaseUseCase<CreateUserDto, UserEntity>
    {
        private IApplicationDbContext _context;
        private PasswordService passwordService; 

        public CreateUser(
            IApplicationDbContext dbContext, 
            PasswordService pswdService)
        {
            passwordService = pswdService;
            _context = dbContext;
        }

        public async Task<UserEntity> Execute(CreateUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.EmailAddress); 
            if (user != null)
            {
                throw new EntityAlreadyExists(nameof(UserEntity), dto.EmailAddress, "EmailAddress"); 
            }

            var newUser = UserEntity.Create(
                UserName: dto.UserName, 
                email: dto.EmailAddress, 
                password: dto.Password, 
                passwordService: passwordService, 
                role: dto.Role ?? Domain.User.Enums.UserRoles.User
            ); 

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser; 
        }
    }
}
