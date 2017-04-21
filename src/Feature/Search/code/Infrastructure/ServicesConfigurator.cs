namespace Sitecore.Feature.Search.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Abstractions;
    using Sitecore.DependencyInjection;
    using Sitecore.Feature.Search.Repositories;
    using Sitecore.Foundation.DependencyInjection;

    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.Add(new ServiceDescriptor(typeof(ISearchContextRepository), typeof(SearchContextRepository), ServiceLifetime.Singleton));
        }
    }
}