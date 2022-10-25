using eCom.CodingChallenge;
using eCom.CodingChallenge.Domain.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace eCom.IntegrationTests.Setup
{
    public class eComFactory : WebApplicationFactory<Startup>
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var bnfsaDbDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<eComDbContext>));


                services.AddDbContext<eComDbContext>(options =>
                    options.UseInMemoryDatabase("eComInMemoryIntegrationTestDB"));


                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var eComDb = scopedServices.GetRequiredService<eComDbContext>();

                    eComDb.Database.EnsureDeleted();
                    eComDb.Database.EnsureCreated();
                }
            });
        }

        public void ExecOnService<T2>(Action<T2> action)
        {
            using (var scope = Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetService<T2>();
                action(service);
            }
        }
    }
}
