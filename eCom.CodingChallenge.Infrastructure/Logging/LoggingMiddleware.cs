using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace eCom.CodingChallenge.Infrastructure.Logging
{
    public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;
		private ILogger<LoggingMiddleware> _logger;

		public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception e)
			{
				_logger.LogError(e, "Unhandled exception");
				throw;
			}
		}
	}
}
