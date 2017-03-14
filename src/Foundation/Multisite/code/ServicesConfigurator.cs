namespace Sitecore.Foundation.Multisite
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.Abstractions;
    using Sitecore.DependencyInjection;
    using Sitecore.Foundation.Multisite.Placeholders;

    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<BasePlaceholderCacheManager, SiteSpecificPlaceholderCacheManager>();
        }
    }
}