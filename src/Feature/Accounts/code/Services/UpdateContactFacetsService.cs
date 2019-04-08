namespace Sitecore.Feature.Accounts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Sitecore.Abstractions;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Feature.Accounts.Services.FacetUpdaters;
    using Sitecore.Foundation.Accounts.Providers;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Security;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Client;
    using Sitecore.XConnect.Client.Configuration;
    using Sitecore.XConnect.Collection.Model;

    [Service(typeof(IUpdateContactFacetsService))]
    public class UpdateContactFacetsService : IUpdateContactFacetsService
    {
        private readonly IXdbContextFactory xdbContextFactory;
        private readonly IContactManagerService contactManager;
        private readonly IList<IContactFacetUpdater> facetUpdaters;

        public UpdateContactFacetsService(IXdbContextFactory xdbContextFactory, IContactManagerService contactManager, IList<IContactFacetUpdater> facetUpdaters)
        {
            Assert.ArgumentNotNull(xdbContextFactory, nameof(xdbContextFactory));
            Assert.ArgumentNotNull(contactManager, nameof(contactManager));
            Assert.ArgumentNotNull(facetUpdaters, nameof(facetUpdaters));
            Assert.ArgumentCondition(facetUpdaters.Any(), nameof(facetUpdaters), $"{nameof(facetUpdaters)} must not be empty");
            this.xdbContextFactory = xdbContextFactory;
            this.contactManager = contactManager;
            this.facetUpdaters = facetUpdaters;
        }

        public void UpdateContactFacets(UserProfile profile)
        {
            var id = this.GetContactId();
            if (id == null)
            {
                return;
            }

            var contactReference = new IdentifiedContactReference(id.Source, id.Identifier);
            var facetsToUpdate = this.facetUpdaters.SelectMany(x => x.FacetsToUpdate).ToArray();

            using (var client = this.xdbContextFactory.CreateContext())
            {
                try
                {
                    var contact = client.Get(contactReference, new ContactExpandOptions(facetsToUpdate));
                    if (contact == null)
                    {
                        return;
                    }
                    var changed = this.facetUpdaters.Select(x => x.SetFacets(profile, contact, client)).ToList().Any();
                    if (changed)
                    {
                        client.Submit();
                        this.contactManager.ReloadContact();
                    }
                }
                catch (XdbExecutionException ex)
                {
                    Log.Error($"Could not update the xConnect contact facets", ex, this);
                }
            }
        }

        private Analytics.Model.Entities.ContactIdentifier GetContactId()
        {
            if (Tracker.Current?.Contact == null)
            {
                return null;
            }
            this.contactManager.SaveContact();
            return Tracker.Current.Contact.Identifiers.FirstOrDefault();
        }
    }
}