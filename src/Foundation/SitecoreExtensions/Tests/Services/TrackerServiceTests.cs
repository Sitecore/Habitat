namespace Sitecore.Foundation.SitecoreExtensions.Tests.Services
{
  using System;
  using System.Collections.Generic;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Analytics.Outcome.Extensions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Marketing.Definitions;
  using Xunit;

  public class TrackerServiceTests
  {
    [Theory, AutoDbData]
    public void TrackPageEvent_ValidID_ShouldTrackById(Database db, [Content] Item item, ITracker tracker, TrackerService trackerService)
    {
      tracker.IsActive.Returns(true);
      using (new TrackerSwitcher(tracker))
      {
        trackerService.TrackPageEvent(item.ID.ToGuid());
        tracker.CurrentPage.Received(1).Register(Arg.Is<PageEventData>(x => x.ItemId == item.ID.ToGuid()));
      }
    }

    [Theory, AutoDbData]
    public void TrackPageEvent_NullTracker_ShouldNotTrackEvent(Database db, [Content] Item item, ITracker tracker, TrackerService trackerService)
    {
      trackerService.TrackPageEvent(item.ID.ToGuid());
      tracker.CurrentPage.DidNotReceive().Register(Arg.Is<PageEventData>(x => x.ItemId == item.ID.ToGuid()));
    }

    [Theory, AutoDbData]
    public void TrackPageEvent_InActiveTracker_ShouldNotTrack(Database db, [Content] Item item, ITracker tracker, TrackerService trackerService)
    {
      using (new TrackerSwitcher(tracker))
      {
        trackerService.TrackPageEvent(item.ID.ToGuid());
        tracker.CurrentPage.DidNotReceive().Register(Arg.Is<PageEventData>(x => x.ItemId == item.ID.ToGuid()));
      }
    }

    [Theory]
    [AutoDbData]
    public void IdentifyContact_ValidIdentifier_ShouldIdentifyContact([NoAutoProperties] TrackerService trackerService, string contactSource, string contactIdentifier, ITracker tracker, [Substitute] Session session)
    {
      tracker.IsActive.Returns(true);
      tracker.Session.Returns(session);
      using (new TrackerSwitcher(tracker))
      {
        trackerService.IdentifyContact(contactSource, contactIdentifier);
        tracker.Session.Received().IdentifyAs(contactSource, contactIdentifier);
      }
    }

    [Theory]
    [AutoDbData]
    public void TrackOutcome_ValidOutcome_ShouldRegisterOutcome([Frozen]ID outcomeDefinitionId, [NoAutoProperties] TrackerService trackerService, ITracker tracker, Contact contact, CurrentInteraction interaction)
    {
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      tracker.Interaction.Returns(interaction);
      tracker.Session.Returns(Substitute.For<Session>());
      tracker.Session.CustomData.Returns(new Dictionary<string, object>());

      using (new TrackerSwitcher(tracker))
      {
        trackerService.TrackOutcome(outcomeDefinitionId.ToGuid());

        tracker.GetContactOutcomes().Should().Contain(o => o.DefinitionId == outcomeDefinitionId);
      }
    }

    [Theory]
    [AutoDbData]
    public void TrackOutcome_InactiveTracker_ShouldNotTrack([Frozen]ID outcomeDefinitionId, [NoAutoProperties] TrackerService trackerService, ITracker tracker)
    {
      // Arrange
      tracker.IsActive.Returns(false);
      tracker.Session.Returns(Substitute.For<Session>());
      tracker.Session.CustomData.Returns(new Dictionary<string, object>());

      using (new TrackerSwitcher(tracker))
      {
        // Act
        trackerService.TrackOutcome(outcomeDefinitionId.ToGuid());

        // Assert
        tracker.GetContactOutcomes().Should().BeEmpty();
      }
    }
  }
}
