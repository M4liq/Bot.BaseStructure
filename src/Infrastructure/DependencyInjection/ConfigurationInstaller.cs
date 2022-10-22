using System;
using System.IO;
using Autofac;
using Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DependencyInjection
{
    public static class ConfigurationInstaller
    {
        public static ContainerBuilder InitializeConfiguration(this ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            RegisterSettings(containerBuilder, configuration);
            return containerBuilder;
        }

        public static IConfigurationBuilder AddConfiguration(this ConfigurationBuilder configurationBuilder) =>
            configurationBuilder
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile(Path.Combine("Configuration","appsettings.json"), true, true)
                .AddJsonFile(Path.Combine("Configuration","proxies.json"), true, true);


        public static ContainerBuilder RegisterSettings(ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            containerBuilder.Register(_ => configuration.GetSection("Proxies").Get<ProxySettings>())
                .As<IProxySettings>().SingleInstance();
            
            containerBuilder.Register(_ => configuration.GetSection("ChromiumSettings").Get<ChromiumSettings>())
                .As<IChromiumSettings>().SingleInstance();

            return containerBuilder;
        }
    }
}
