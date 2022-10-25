using Microsoft.Extensions.Configuration;

namespace eCom.CodingChallenge.Infrastructure.Settings
{
    public interface IeComSettings
    {
        string eComDB { get; }
    }
    public class eComSettings : IeComSettings
    {
        private readonly IConfiguration _configuration;

        public eComSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string eComDB => _configuration.GetConnectionString(nameof(eComDB));
    }
}
    