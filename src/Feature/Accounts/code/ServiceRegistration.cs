namespace Sitecore.Feature.Accounts
{
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Sitecore.DependencyInjection;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Feature.Accounts.Services.FacetUpdaters;

    public class ServiceRegistration : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IList<IContactFacetUpdater>>(provider => new List<IContactFacetUpdater>()
            {
                new PersonalInformationFacetUpdater(),
                new PhoneFacetUpdater(),
                new EmailFacetUpdater(),
                new AvatarFacetUpdater(new WebClient())
            });
        }
    }
}