using Microsoft.EntityFrameworkCore;
using NameApp.Application.Common.Interactors;
using NameApp.Application.Common.Interfaces;
using NameApp.Application.User.Dto;
using NameApp.Application.User.Exceptions;
using NameApp.Domain.AccessService.Services;
using NameApp.Domain.User.Entities;
using NameApp.Domain.User.Services;

namespace NameApp.Application.User.Interactors
{
    public class RegisterInteractor : BaseInteractor<CreateUserDto, UserEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly UserEntityService _userService;
        private readonly PermissionEntityService _permissionService; 

        public RegisterInteractor(
            IApplicationDbContext dbContext, 
            UserEntityService userEntityService, 
            PermissionEntityService permissionService)
        {
            _context = dbContext;
            _userService = userEntityService;
            _permissionService = permissionService;
        }

        public async Task<UserEntity> Execute(CreateUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.EmailAddress); 
            if (user != null)
            {
                throw new UserAlreadyExists(dto.EmailAddress, "EmailAddress"); 
            }
            var userPermission = _permissionService.CreateUserPermission(); 

            var newUser = _userService.Create(
                UserName: dto.UserName, 
                email: dto.EmailAddress, 
                password: dto.Password, 
                permission: userPermission 
            ); 

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser; 
        }
    }
}
