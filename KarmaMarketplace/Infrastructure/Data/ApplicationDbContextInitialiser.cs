using KarmaMarketplace.Application.User.Dto;
using KarmaMarketplace.Application.User.Interfaces;
using KarmaMarketplace.Domain.Payment.Entities;
using KarmaMarketplace.Domain.User.Entities;
using KarmaMarketplace.Domain.User.Enums;
using KarmaMarketplace.Infrastructure.Adapters.Payment;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SKitLs.Bots.Telegram.Core.Users;
using System.Data;

namespace KarmaMarketplace.Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.InitialiseAsync();

            await initialiser.SeedAsync(); 
        }
    }

    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService; 

        public ApplicationDbContextInitialiser(
            ILogger<ApplicationDbContextInitialiser> logger,
            ApplicationDbContext context, 
            IUserService userService )
        {
            _logger = logger;
            _context = context;
            _userService = userService;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default users
            var administrator = new UserEntity { UserName = "admin", Email = "admin@localhost" };

            if (_context.Users.All(u => u.UserName != administrator.UserName))
            {
                await _userService.Create().Execute(new CreateUserDto()
                {
                    UserName = administrator.UserName,
                    EmailAddress = administrator.Email,
                    Password = administrator.UserName,
                    Role = UserRoles.SuperAdmin
                });
            }

            // Default data
            // Seed, if necessary
            if (!_context.PaymentProviders.Any())
            {
                var providers = new List<TransactionProviderEntity>
                {
                    new()
                    {
                        Name = nameof(PaymentProviders.BankCardRu),
                        Systems = [new PaymentSystemEntity() { Name = "paypalych" }]
                    }, 
                    new() {
                        Name = nameof(PaymentProviders.Balance), 
                        Systems = []
                    }
                };

                _context.PaymentProviders.AddRange(providers); 

                await _context.SaveChangesAsync();
            }
        }
    }
}
