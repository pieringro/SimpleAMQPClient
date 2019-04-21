using System.IO;
using Microsoft.Extensions.Configuration;

namespace SimpleAMQPWrapper {
    public class Settings {
        protected readonly string appSettingsJsonNameFile = "appsettings.json";
        protected IConfigurationRoot Configuration { get; set; }
        protected IConfigurationSection ConfigurationSection { get; set; }
        public bool refreshInstance = false;
        protected void buildConfigurations(string section) {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(appSettingsJsonNameFile);

            Configuration = builder.Build();
            ConfigurationSection = Configuration.GetSection(section);
        }
    }
}