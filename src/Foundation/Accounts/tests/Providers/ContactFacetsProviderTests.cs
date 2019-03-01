namespace Sitecore.Foundation.Accounts.Tests.Providers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using FluentAssertions;
    using NSubstitute;
    using Ploeh.AutoFixture.AutoNSubstitute;
    using Ploeh.AutoFixture.Xunit2;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Analytics.XConnect.Facets;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.XConnect.Collection.Model;
    using Xunit;
    using ContactFacetsProvider = Sitecore.Foundation.Accounts.Providers.ContactFacetsProvider;
    using Facet = Sitecore.XConnect.Facet;

    public class ContactFacetsProviderTests
  {
    [Theory]
    [AutoDbData]
    public void Contact_ContactInitialized_ShouldReturnContact([NoAutoProperties] ContactFacetsProvider provider, ITracker tracker, Contact contact)
    {
      // Arrange
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      using (new TrackerSwitcher(tracker))
      {
        // Act / Assert
        tracker.Contact.Should().NotBeNull();
        provider.Contact.Should().BeEquivalentTo(tracker.Contact);
      }
    }


    [Theory]
    [AutoDbData]
    public void Addresses_FacetExists_ShouldReturnContactAddressProperty([NoAutoProperties] ContactFacetsProvider provider, IXConnectFacets facets, AddressList facet, ITracker tracker, [Substitute] Contact contact)
    {
      // Arrange
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      facets.Facets.Returns(new ReadOnlyDictionary<string, Facet>(new Dictionary<string, Facet>
      {
        {
            AddressList.DefaultFacetKey, facet
        }
      }));
      contact.GetFacet<IXConnectFacets>("XConnectFacets").Returns(facets);

      using (new TrackerSwitcher(tracker))
      {
        // Act
        var addresses = provider.Addresses;
        
        // Assert
        addresses.Should().NotBeNull();
        addresses.Should().BeEquivalentTo(facet);
      }
    }


    [Theory]
    [AutoDbData]
    public void BehaviorProfiles_FacetExists_ShouldReturnContactBehaviorProfiles([NoAutoProperties] ContactFacetsProvider provider, IContactBehaviorProfilesContext facet, ITracker tracker, [Substitute] Contact contact)
    {
      // Arrange
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      contact.BehaviorProfiles.Returns(facet);

      using (new TrackerSwitcher(tracker))
      {
        // Act / Assert
        provider.BehaviorProfiles.Should().NotBeNull();
        provider.BehaviorProfiles.Should().BeEquivalentTo(facet.Profiles);
      }
    }

    [Theory]
    [AutoDbData]
    public void CommunicationProfile_FacetExists_ShouldReturnContactCommunicationProfile([NoAutoProperties] ContactFacetsProvider provider, IXConnectFacets facets, ConsentInformation facet, ITracker tracker, [Substitute] Contact contact)
    {
      // Arrange
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      facets.Facets.Returns(new ReadOnlyDictionary<string, Facet>(new Dictionary<string, Facet>
      {
        {
           ConsentInformation.DefaultFacetKey, facet
        }
      }));
      contact.GetFacet<IXConnectFacets>("XConnectFacets").Returns(facets);

      using (new TrackerSwitcher(tracker))
      {
        // Act
        var profile = provider.CommunicationProfile;

        // Assert
        profile.Should().NotBeNull();
        profile.Should().BeEquivalentTo(facet);
      }
    }


    [Theory]
    [AutoDbData]
    public void Emails_FacetExist_ShouldReturnContactEmail([NoAutoProperties] ContactFacetsProvider provider, IXConnectFacets facets, EmailAddressList facet, ITracker tracker, [Substitute] Contact contact)
    {
      // Arrange
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      facets.Facets.Returns(new ReadOnlyDictionary<string, Facet>(new Dictionary<string, Facet>
      {
        {
          EmailAddressList.DefaultFacetKey, facet
        }
      }));
      contact.GetFacet<IXConnectFacets>("XConnectFacets").Returns(facets);

      using (new TrackerSwitcher(tracker))
      {
        // Act
        var emails = provider.Emails;
        
        // Assert
        emails.Should().NotBeNull();
        emails.Should().BeEquivalentTo(facet);
      }
    }

    [Theory]
    [AutoDbData]
    public void KeyBehaviorCache_WhenNotExist_ShouldReturnNull([NoAutoProperties] ContactFacetsProvider provider, ITracker tracker, [Substitute] Contact contact)
    {
      // Arrange
      tracker.IsActive.Returns(true);
      contact.Attachments.Clear();
      tracker.Contact.Returns(contact);

      using (new TrackerSwitcher(tracker))
      {
        // Act / Assert
        provider.KeyBehaviorCache.Should().BeNull();
      }
    }
  }
}