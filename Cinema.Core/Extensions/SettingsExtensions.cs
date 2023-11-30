using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cinema.Core.Extensions
{
    public static class SettingsExtensions
    {
        public static TClass GetSettings<TClass>(this IConfiguration configuration)
            where TClass : class, new()
        {
            var section = typeof(TClass).Name.Replace("Settings", string.Empty, StringComparison.InvariantCulture);
            TClass configurationValue = new();
            var result = configuration.GetSection(section);

            if (!result.Exists())
            {
                throw new ArgumentException("No settings " + section);
            }

            result.Bind(configurationValue);

            return configurationValue;
        }

        public static TClass RegisterSettings<TClass>(this IServiceCollection services, IConfiguration configuration)
            where TClass : class, new()
        {
            var settings = configuration.GetSettings<TClass>();
            services.AddSingleton(settings);
            return settings;
        }
    }
}
