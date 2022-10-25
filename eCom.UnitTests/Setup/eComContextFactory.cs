using eCom.CodingChallenge;
using eCom.CodingChallenge.Domain.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eCom.UnitTests.Setup
{
    public class eComContextFactory
    {
        public static eComDbContext CreateContextForInMemoryDB()
        {
            var options = new DbContextOptionsBuilder<eComDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryUnitTestDB")
                .Options;

            var context = new eComDbContext(options);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
