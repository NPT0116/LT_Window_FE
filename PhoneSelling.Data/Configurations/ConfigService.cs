using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Configurations
{
    public class ConfigService : IConfigService
    {
        private readonly IConfiguration _configuration;

        public ConfigService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Get executing path
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build();
        }

        public T GetSection<T>(string sectionName) where T : new()
        {
            var section = new T();
            _configuration.GetSection(sectionName).Bind(section);
            return section;
        }
    }
}
