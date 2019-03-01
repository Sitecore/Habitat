namespace Sitecore.Foundation.SitecoreExtensions.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using NSubstitute;
    using Ploeh.AutoFixture.AutoNSubstitute;
    using Ploeh.AutoFixture.Xunit2;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb.AutoFixture;
    using Sitecore.Foundation.SitecoreExtensions.Services;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.Marketing.Definitions.Outcomes.Model;
    using Sitecore.Marketing.Definitions.PageEvents;
    using Xunit;

    public class TrackerServiceTests
  {
    [Theory, AutoDbData]
    public void TrackPageEvent_ValidID_ShouldTrackById(Database db, [Content] Item item, ITracker tracker, TrackerService trackerService, IPageEventDefinition pageEvent)
    {
      // Arrange
      trackerService.PageEventDefinitionManager.Get(Arg.Any<Guid>(), Arg.Any<CultureInfo>()).Returns(info =>
      {
        pageEvent.Id.Returns((Guid)info[0]);
        return pageEvent;
      });
      tracker.IsActive.Returns(true);
      using (new TrackerSwitcher(tracker))
      {
        // Act
        trackerService.TrackPageEvent(item.ID.ToGuid());

        // Assert
        tracker.CurrentPage.Received(1).RegisterPageEvent(Arg.Is<IPageEventDefinition>(x => x.Id == item.ID.ToGuid()));
      }
    }

    [Theory, AutoDbData]
    public void TrackPageEvent_NullTracker_ShouldNotTrackEvent(Database db, [Content] Item item, ITracker tracker, TrackerService trackerService)
    {
      // Act
      trackerService.TrackPageEvent(item.ID.ToGuid());

      // Assert
      tracker.CurrentPage.DidNotReceiveWithAnyArgs().RegisterPageEvent(Arg.Any<IPageEventDefinition>());
    }

    [Theory, AutoDbData]
    public void TrackPageEvent_InActiveTracker_ShouldNotTrack(Database db, [Content] Item item, ITracker tracker, TrackerService trackerService)
    {
      // Arrange
      tracker.IsActive.Returns(false);
      using (new TrackerSwitcher(tracker))
      {
        // Act
        trackerService.TrackPageEvent(item.ID.ToGuid());

        // Assert
        tracker.CurrentPage.DidNotReceiveWithAnyArgs().RegisterPageEvent(Arg.Any<IPageEventDefinition>());
      }
    }

    [Theory]
    [AutoDbData]
    public void IdentifyContact_ValidIdentifier_ShouldIdentifyContact([NoAutoProperties] TrackerService trackerService, string contactSource, string contactIdentifier, ITracker tracker, [Substitute] Session session)
    {
      // Arrange
      tracker.IsActive.Returns(true);
      tracker.Session.Returns(session);
      using (new TrackerSwitcher(tracker))
      {
        // Act
        trackerService.IdentifyContact(contactSource, contactIdentifier);

        // Assert
        tracker.Session.Received().IdentifyAs(contactSource, contactIdentifier);
      }
    }

    [Theory]
    [AutoDbData]
    public void TrackOutcome_ValidOutcome_ShouldRegisterOutcome([Frozen]Guid outcomeDefinitionId, TrackerService trackerService, ITracker tracker, Contact contact, IOutcomeDefinition outcome)
    {
      // Arrange
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      trackerService.OutcomeDefinitionManager.Get(Arg.Any<Guid>(), Arg.Any<CultureInfo>()).Returns(info =>
      {
        outcome.Id.Returns((Guid)info[0]);
        return outcome;
      });

      using (new TrackerSwitcher(tracker))
      {
        // Act
        trackerService.TrackOutcome(outcomeDefinitionId);

        // Assert
        tracker.CurrentPage.Received(1).RegisterOutcome(Arg.Is<IOutcomeDefinition>(x => x.Id == outcomeDefinitionId), Arg.Any<string>(), Arg.Any<decimal>());
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
        tracker.CurrentPage.DidNotReceiveWithAnyArgs().RegisterOutcome(Arg.Any<IOutcomeDefinition>(), Arg.Any<string>(), Arg.Any<decimal>());
      }
    }
  }
}
