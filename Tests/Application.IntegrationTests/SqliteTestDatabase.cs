using KarmaMarketplace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using KarmaMarketplace.Infrastructure.EventDispatcher;

namespace Tests.Application.IntegrationTests
{
    public class SqliteTestDatabase : ITestDatabase
    {
        private readonly string _connectionString;
        private readonly SqliteConnection _connection;

        public SqliteTestDatabase()
        {
            _connectionString = "DataSource=:memory:";
            _connection = new SqliteConnection(_connectionString);
        }

        public async Task InitialiseAsync()
        {
            if (_connection.State == ConnectionState.Open)
            {
                await _connection.CloseAsync();
            }

            await _connection.OpenAsync();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;

            var context = new ApplicationDbContext(
                options, 
                eventDispatcher: EventDispatcherMockFactory.Create().Object);

            context.Database.Migrate();
        }

        public DbConnection GetConnection()
        {
            return _connection;
        }

        public async Task ResetAsync()
        {
            await InitialiseAsync();
        }

        public async Task DisposeAsync()
        {
            await _connection.DisposeAsync();
        }
    }
}
