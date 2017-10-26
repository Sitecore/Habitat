namespace Sitecore.Foundation.Accounts.Providers
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Analytics.XConnect.Facets;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Shell.Framework.Commands.Masters;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Client;
    using Sitecore.XConnect.Client.Configuration;
    using Sitecore.XConnect.Collection.Model;

    [Service(typeof(IContactFacetsProvider), Lifetime = Lifetime.Transient)]
    public class ContactFacetsProvider : IContactFacetsProvider
    {
        private readonly ContactManager contactManager;

        public ContactFacetsProvider()
        {
            this.contactManager = Factory.CreateObject("tracking/contactManager", true) as ContactManager;
        }

        public IEnumerable<IBehaviorProfileContext> BehaviorProfiles => this.Contact?.BehaviorProfiles.Profiles ?? Enumerable.Empty<IBehaviorProfileContext>();
        public PersonalInformation PersonalInfo => this.GetFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey);
        public AddressList Addresses => this.GetFacet<AddressList>(AddressList.DefaultFacetKey);
        public EmailAddressList Emails => this.GetFacet<EmailAddressList>(EmailAddressList.DefaultFacetKey);
        public ConsentInformation CommunicationProfile => this.GetFacet<ConsentInformation>(ConsentInformation.DefaultFacetKey);
        public PhoneNumberList PhoneNumbers => this.GetFacet<PhoneNumberList>(PhoneNumberList.DefaultFacetKey);

        public Analytics.Tracking.Contact Contact
        {
            get
            {
                if (!Tracker.IsActive)
                {
                    return null;
                }

                var contact = Tracker.Current.Contact;
                return contact ?? this.contactManager?.CreateContact(ID.NewID);
            }
        }

        public Avatar Picture => this.GetFacet<Avatar>(Avatar.DefaultFacetKey);

        public bool IsKnown => Tracker.Current?.Contact?.IdentificationLevel == ContactIdentificationLevel.Known;

        public XConnect.Collection.Model.KeyBehaviorCache KeyBehaviorCache => this.GetFacet<XConnect.Collection.Model.KeyBehaviorCache>(XConnect.Collection.Model.KeyBehaviorCache.DefaultFacetKey);

        protected T GetFacet<T>(string facetName) where T : Facet
        {
            var xConnectFacet = Tracker.Current.Contact.GetFacet<IXConnectFacets>("XConnectFacets");
            var allFacets = xConnectFacet.Facets;
            if (allFacets == null)
                return null;
            if (!allFacets.ContainsKey(facetName))
                return null;

            return (T)allFacets?[facetName];
        }
    }
}