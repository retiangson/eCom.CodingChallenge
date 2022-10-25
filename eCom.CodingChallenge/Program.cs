using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace eCom.CodingChallenge
{

    public class Program
    {
        private static IConfiguration _configuration;

        public static async Task<int> Main(string[] args)
        {
            _configuration = BuildConfiguration(args);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger()
                .ForContext<Program>();

            Log.Information("Starting web host");

            try
            {
                await CreateHostBuilder(args).Build().RunAsync();

                return 0;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return 1;
            }
            finally
            {
                Log.Information("Ending web host");

                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>()
                .UseSerilog();
            });

        private static IConfiguration BuildConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                //.AddEnvironmentVariables(prefix: "DOCKER_") // [For Docker]: Values in appsettings.json will be overridden with any environment variables prefixed with DOCKER_
                .AddCommandLine(args)
                .Build();
        }
    }
}
