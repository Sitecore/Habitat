namespace Sitecore.Feature.Accounts.Tests
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using NSubstitute;
    using Sitecore.Abstractions;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Feature.Accounts.Services.FacetUpdaters;
    using Sitecore.Foundation.Accounts.Providers;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.Security;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Collection.Model;
    using Sitecore.XConnect.Operations;
    using Xunit;
    using Contact = Sitecore.Analytics.Tracking.Contact;
    using ContactIdentifier = Sitecore.Analytics.Model.Entities.ContactIdentifier;

    public class UpdateContactFacetsServiceTests
    {
        [Theory]
        [AutoDbData]
        public void UpdateContactFacets_ShouldSubmitToXdb(ITracker tracker, Contact contact, Sitecore.XConnect.Contact xdbContact, IXdbContextFactory xdbContextFactory, IXdbContext xdbContext, IContactManagerService contactManager, IContactFacetUpdater facetUpdater, string source, string identifier)
        {
            // Arrange
            contact.Identifiers.Returns(new List<ContactIdentifier>
            {
                new ContactIdentifier(source, identifier, ContactIdentificationLevel.Known)
            }.AsReadOnly());
            tracker.Contact.Returns(contact);
            xdbContext.GetAsync<Sitecore.XConnect.Contact>(Arg.Any<IdentifiedContactReference>(), Arg.Any<ContactExpandOptions>()).Returns(Task.FromResult(xdbContact));
            xdbContextFactory.CreateContext().Returns(xdbContext);
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            facetUpdater.SetFacets(userProfile, xdbContact, xdbContext).Returns(true);
            var facetsService = new UpdateContactFacetsService(xdbContextFactory, contactManager, new List<IContactFacetUpdater> { facetUpdater });

            // Act
            using (new TrackerSwitcher(tracker))
            {
                facetsService.UpdateContactFacets(userProfile);
            }

            // Assert
            xdbContext.Received().SubmitAsync();
        }

        [Theory]
        [AutoDbData]
        public void UpdateContactFacets_ShouldUpdateTracker(ITracker tracker, Contact contact, Sitecore.XConnect.Contact xdbContact, IXdbContextFactory xdbContextFactory, IXdbContext xdbContext, IContactManagerService contactManager, IContactFacetUpdater facetUpdater, string source, string identifier)
        {
            // Arrange
            contact.Identifiers.Returns(new List<ContactIdentifier>
            {
                new ContactIdentifier(source, identifier, ContactIdentificationLevel.Known)
            }.AsReadOnly());
            tracker.Contact.Returns(contact);
            xdbContext.GetAsync<Sitecore.XConnect.Contact>(Arg.Any<IdentifiedContactReference>(), Arg.Any<ContactExpandOptions>()).Returns(Task.FromResult(xdbContact));
            xdbContextFactory.CreateContext().Returns(xdbContext);
            var userProfile = Substitute.For<Sitecore.Security.UserProfile>();
            facetUpdater.SetFacets(userProfile, xdbContact, xdbContext).Returns(true);
            var facetsService = new UpdateContactFacetsService(xdbContextFactory, contactManager, new List<IContactFacetUpdater> { facetUpdater });

            // Act
            using (new TrackerSwitcher(tracker))
            {
                facetsService.UpdateContactFacets(userProfile);
            }

            // Assert
            contactManager.Received(1).SaveContact();
            contactManager.Received(1).ReloadContact();
        }
    }
}
