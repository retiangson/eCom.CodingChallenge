using eCom.CodingChallenge.Business.Services;
using eCom.CodingChallenge.Domain;
using eCom.CodingChallenge.Infrastructure.Logging;
using eCom.CodingChallenge.Infrastructure.Settings;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace eCom.CodingChallenge
{
    public class Startup
    {
        private readonly IConfiguration Configuration;
        private readonly Version Version = Assembly.GetExecutingAssembly().GetName().Version;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var settings = new eComSettings(Configuration);
            services.AddSingleton<IeComSettings>(settings);

            services.AddLogging(options =>
            {
#if DEBUG
                options.AddDebug();
#endif
#if !DEBUG
				options.AddFilter((LogLevel level) =>
				{
					return level == LogLevel.Critical
						|| level == LogLevel.Error
						|| level == LogLevel.Warning;
				});
#endif

                options.AddApplicationInsights();
                options.AddFilter<ApplicationInsightsLoggerProvider>((LogLevel level) =>
                {
                    return level == LogLevel.Critical
                        || level == LogLevel.Error
                        || level == LogLevel.Warning
                        || level == LogLevel.Information;
                });
                options.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning);
            });

            // controllers
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            });

            // services & repositories
            services.AddServices();
            services.AddDomain(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // application insights
            services.AddApplicationInsightsTelemetry();

            services.AddHealthChecks();

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Title = "eCom.CodingChallenge";
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // for all unhandled exceptions
            app.UseMiddleware<LoggingMiddleware>();

            // Enable middle ware to serve generated Swagger as a JSON endpoint.
            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseRouting();

            app.UseCors("Open");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                var options = new HealthCheckOptions
                {
                    ResponseWriter = async (c, r) =>
                    {
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new
                        {
                            status = r.Status.ToString(),
                            version = Version.ToString(),
                            totalDuration = r.TotalDuration.ToString(),
                            entries = JsonConvert.SerializeObject(r.Entries)
                        });
                        await c.Response.WriteAsync(result);
                    }
                };
                endpoints.MapHealthChecks("/api/healthcheck", options);
            });
        }
    }
}
