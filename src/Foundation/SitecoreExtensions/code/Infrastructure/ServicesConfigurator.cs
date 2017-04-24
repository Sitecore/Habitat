namespace Sitecore.Foundation.SitecoreExtensions.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;
    using Sitecore.Foundation.SitecoreExtensions.Repositories;

    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.Add(new ServiceDescriptor(typeof(IRenderingPropertiesRepository), typeof(RenderingPropertiesRepository), ServiceLifetime.Singleton));
        }
    }
}