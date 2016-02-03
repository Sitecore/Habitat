namespace Sitecore.Foundation.SitecoreExtensions.Tests.Services
{
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Analytics.Model.Framework;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class ContactProfileProviderTests
  {
    [Theory]
    [AutoDbData]
    public void Contact_ContactInitialized_ShouldReturnContact([NoAutoProperties] ContactProfileProvider provider, ITracker tracker, Contact contact)
    {
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      using (new TrackerSwitcher(tracker))
      {
        tracker.Contact.Should().NotBeNull();
        provider.Contact.ShouldBeEquivalentTo(tracker.Contact);
      }
    }


    [Theory]
    [AutoDbData]
    public void Addresses_FacetExists_ShouldReturnContactAddressProperty([NoAutoProperties] ContactProfileProvider provider, IContactAddresses facet, ITracker tracker, [Substitute] Contact contact)
    {
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      contact.Facets.Returns(new ReadOnlyDictionary<string, IFacet>(new Dictionary<string, IFacet>
      {
        {
          @"Address", facet
        }
      }));

      using (new TrackerSwitcher(tracker))
      {
        provider.Addresses.Should().NotBeNull();
        provider.Addresses.ShouldBeEquivalentTo(facet);
      }
    }


    [Theory]
    [AutoDbData]
    public void BehaviorProfiles_FacetExists_ShouldReturnContactBehaviorProfiles([NoAutoProperties] ContactProfileProvider provider, IContactBehaviorProfilesContext facet, ITracker tracker, [Substitute] Contact contact)
    {
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      contact.BehaviorProfiles.Returns(facet);

      using (new TrackerSwitcher(tracker))
      {
        provider.BehaviorProfiles.Should().NotBeNull();
        provider.BehaviorProfiles.ShouldBeEquivalentTo(facet.Profiles);
      }
    }

    [Theory]
    [AutoDbData]
    public void CommunicationProfile_FacetExists_ShouldReturnContactCommunicationProfile([NoAutoProperties] ContactProfileProvider provider, IContactCommunicationProfile facet, ITracker tracker, [Substitute] Contact contact)
    {
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      contact.Facets.Returns(new ReadOnlyDictionary<string, IFacet>(new Dictionary<string, IFacet>
      {
        {
          @"Communication Profile", facet
        }
      }));

      using (new TrackerSwitcher(tracker))
      {
        provider.CommunicationProfile.Should().NotBeNull();
        provider.CommunicationProfile.ShouldBeEquivalentTo(facet);
      }
    }


    [Theory]
    [AutoDbData]
    public void Emails_FacetExist_ShouldReturnContactEmail([NoAutoProperties] ContactProfileProvider provider, IContactEmailAddresses facet, ITracker tracker, [Substitute] Contact contact)
    {
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      contact.Facets.Returns(new ReadOnlyDictionary<string, IFacet>(new Dictionary<string, IFacet>
      {
        {
          @"Emails", facet
        }
      }
        ));

      using (new TrackerSwitcher(tracker))
      {
        provider.Emails.Should().NotBeNull();
        provider.Emails.ShouldBeEquivalentTo(facet);
      }
    }

    [Theory]
    [AutoDbData]
    public void KeyBehaviorCache_WhenNotExist_ShouldReturnNull([NoAutoProperties] ContactProfileProvider provider, ITracker tracker, [Substitute] Contact contact)
    {
      tracker.IsActive.Returns(true);
      contact.Attachments.Clear();
      tracker.Contact.Returns(contact);

      using (new TrackerSwitcher(tracker))
      {
        provider.KeyBehaviorCache.Should().BeNull();
      }
    }
  }
}