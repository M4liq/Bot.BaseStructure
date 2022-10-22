using Application.Interfaces;
using Autofac;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static ContainerBuilder RegisterInfrastructure(this ContainerBuilder containerBuilder, IConfiguration configuration)
        {
            containerBuilder.InitializeConfiguration(configuration);

            containerBuilder.RegisterType<BrowserService>().As<IBrowserService>().SingleInstance();
            
            return containerBuilder;
        }
    }
}
