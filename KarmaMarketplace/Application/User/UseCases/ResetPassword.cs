using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Infrastructure;
using KarmaMarketplace.Infrastructure.Adapters.Mailing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace KarmaMarketplace.Application.User.UseCases
{
    public class ResetPassword : BaseUseCase<ResetPasswordDto, bool>
    {
        private IApplicationDbContext _context;
        private IMemoryCache _cacheService;
        private PasswordService _passwordService; 

        public ResetPassword(
            IApplicationDbContext dbContext,
            IMemoryCache cacheService, 
            PasswordService passwordService) {
            _context = dbContext; 
            _cacheService = cacheService;
            _passwordService = passwordService;
        }

        public async Task<bool> Execute(ResetPasswordDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            Guard.Against.Null(user, message: "User does not exists");

            var rawCode = _cacheService.Get(user.Email + dto.Code);

            Guard.Against.Null(rawCode, message: "Reset password code is empty");

            try
            {
                var code = (int)rawCode;
            } catch (InvalidCastException)
            {
                throw new Exception($"Что то пошло очень не так, code: {rawCode}"); 
            }

            user.UpdatePassword(dto.NewPassword, _passwordService);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return true; 
        }
    }   
}
