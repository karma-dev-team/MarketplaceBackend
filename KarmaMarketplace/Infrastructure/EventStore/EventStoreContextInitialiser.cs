using KarmaMarketplace.Application.Files.Interfaces;
using KarmaMarketplace.Application.User.Interfaces;
using KarmaMarketplace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KarmaMarketplace.Infrastructure.EventSourcing
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseEventStoreDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<EventStoreContextInitialiser>();

            await initialiser.InitialiseAsync();
        }
    }

    public class EventStoreContextInitialiser
    {
        private readonly ILogger<EventStoreContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;

        public EventStoreContextInitialiser(
            ILogger<EventStoreContextInitialiser> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the event store database.");
                throw;
            }
        }
    }
}
