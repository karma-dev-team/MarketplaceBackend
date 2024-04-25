using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Application.IntegrationTests
{
    public static class TestDatabaseFactory
    {
        public static async Task<ITestDatabase> CreateAsync()
        {
// I DONT KNOW HOW TO MAKE AND SET VALUES OF DIRECTIVES VARIABLES... 
//#if (UseSQLite)
        var database = new SqliteTestDatabase();
//#else
//#if DEBUG
//            var database = new PostgresqlTestDatabase();
//#endif
            await database.InitialiseAsync();

            return database;
        }
    }
}
