using System.Data.Common;
using KarmaMarketplace.Application.Common.Interfaces;
using KarmaMarketplace.Infrastructure.Data;
using KarmaMarketplace.Infrastructure.EventSourcing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using static Tests.Application.IntegrationTests.Testing; 

namespace Tests.Application.IntegrationTests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly DbConnection _connection;

        public CustomWebApplicationFactory(DbConnection connection)
        {
            _connection = connection;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services
                    .RemoveAll<IUser>()
                    .AddTransient(provider => Mock.Of<IUser>(s => s.Id == GetUserId()));

                services
                    .RemoveAll<DbContextOptions<ApplicationDbContext>>()
                    .AddDbContext<ApplicationDbContext>((sp, options) =>
                    {
                        options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                        options.UseNpgsql(
                            _connection,
                            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                    });

                services
                    .RemoveAll<DbContextOptions<EventStoreContext>>()
                    .AddDbContext<EventStoreContext>((sp, options) =>
                    {
                        options.EnableDetailedErrors(true);
                        options.UseNpgsql(
                            _connection,
                            o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                        );
                    }); 
            });
        }
    }
}
