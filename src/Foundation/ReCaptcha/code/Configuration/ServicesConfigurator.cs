namespace Sitecore.Foundation.ReCaptcha.Configuration
{
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;
    using Sitecore.Foundation.ReCaptcha.Services;

    public class ServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<ISiteVerifyService>(provider => new SiteVerifyService());
        }
    }
}