namespace Sitecore.Foundation.Indexing.Infrastructure
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;
    using Sitecore.Foundation.Indexing.Repositories;

    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.Add(new ServiceDescriptor(typeof(ISearchServiceRepository), typeof(SearchServiceRepository), ServiceLifetime.Singleton));
        }
    }
}