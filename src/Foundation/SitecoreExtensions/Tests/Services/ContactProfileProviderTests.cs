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
  using Sitecore.Foundation.SitecoreExtensions.Tests.Common;
  using Xunit;

  public class ContactProfileProviderTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldReturnContact([NoAutoProperties] ContactProfileProvider provider, ITracker tracker, Contact contact)
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
    public void ShouldReturnContactAddressProperty([NoAutoProperties] ContactProfileProvider provider, IContactAddresses facet, ITracker tracker, [Substitute] Contact contact)
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
    public void ShouldReturnContactBehaviorProfilesProperty([NoAutoProperties] ContactProfileProvider provider, IContactBehaviorProfilesContext facet, ITracker tracker, [Substitute] Contact contact)
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
    public void ShouldReturnContactCommunicationProfileProperty([NoAutoProperties] ContactProfileProvider provider, IContactCommunicationProfile facet, ITracker tracker, [Substitute] Contact contact)
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
    public void ShouldReturnContactEmailProperty([NoAutoProperties] ContactProfileProvider provider, IContactEmailAddresses facet, ITracker tracker, [Substitute] Contact contact)
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
  }
}