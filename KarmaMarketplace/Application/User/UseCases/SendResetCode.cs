using KarmaMarketplace.Application.Common.Interactors;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Infrastructure.Adapters.Mailing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel;

namespace KarmaMarketplace.Application.User.UseCases
{
    public class SendResetCode : BaseUseCase<SendResetCodeDto, bool>
    {
        private IApplicationDbContext _context;
        private IEmailService _emailService;
        private IMemoryCache _cacheService; 

        public SendResetCode(IEmailService emailService, IApplicationDbContext dbContext, IMemoryCache cacheService) {
            _context = dbContext;
            _emailService = emailService;
            _cacheService = cacheService;
        }

        public async Task<bool> Execute(SendResetCodeDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            Guard.Against.Null("User does not exists"); 
            
            Random rnd = new Random();

            var code = rnd.Next(100000);

            var newCode = _cacheService.Set(dto.Email + code, code); 

            await _emailService.SendEmailAsync(
                dto.Email, 
                "Возврат пароля для входа в ваш аккаунт", 
                $"Ваш код для сброса пароля: {code}"); 

            return true; 
        }
    }
}
