namespace Sitecore.Feature.Accounts.Tests
{
  using System;
  using System.Collections.Generic;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Analytics.Outcome.Extensions;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Accounts.Services;
  using Sitecore.Feature.Accounts.Tests.Extensions;
  using Xunit;

  public class AccountTrackerServiceTests
  {
    [Theory, AutoDbData]
    public void TrackPageEvent_ValidID_ShouldTrackById(Database db, [Content] Item item, ITracker tracker, AccountTrackerService accountTrackerService)
    {
      tracker.IsActive.Returns(true);
      using (new TrackerSwitcher(tracker))
      {
        accountTrackerService.TrackPageEvent(item.ID);
        tracker.CurrentPage.Received(1).Register(Arg.Is<PageEventItem>(x => x.ID == item.ID));
      }
    }

    [Theory, AutoDbData]
    public void TrackPageEvent_NullEvent_ShouldThrowArgumentException(AccountTrackerService accountTrackerService)
    {
      accountTrackerService.Invoking(x => x.TrackPageEvent(null)).ShouldThrow<ArgumentNullException>();
    }

    [Theory, AutoDbData]
    public void TrackPageEvent_NullTracker_ShouldNotTrackEvent(Database db, [Content] Item item, ITracker tracker, AccountTrackerService accountTrackerService)
    {
        accountTrackerService.TrackPageEvent(item.ID);
        tracker.CurrentPage.DidNotReceive().Register(Arg.Is<PageEventItem>(x => x.ID == item.ID));
    }

    [Theory, AutoDbData]
    public void TrackPageEvent_InActiveTracker_ShouldNotTrack(Database db, [Content] Item item, ITracker tracker, AccountTrackerService accountTrackerService)
    {
      using (new TrackerSwitcher(tracker))
      {
        accountTrackerService.TrackPageEvent(item.ID);
        tracker.CurrentPage.DidNotReceive().Register(Arg.Is<PageEventItem>(x => x.ID == item.ID));
      }
    }

    [Theory, AutoDbData]
    public void TrackLogin_Call_ShouldTrackLoginGoal(string identifier, Db db, ITracker tracker, AccountTrackerService accountTrackerService, [Substitute] Session session)
    {
      tracker.IsActive.Returns(true);
      tracker.Session.Returns(session);
      db.Add(new DbItem("Item", ConfigSettings.LoginGoalId));

      using (db)
      using (new TrackerSwitcher(tracker))
      {
        accountTrackerService.TrackLogin(identifier);
        tracker.CurrentPage.Received(1).Register(Arg.Is<PageEventItem>(x => x.ID == ConfigSettings.LoginGoalId));
      }
    }

    [Theory, AutoDbData]
    public void TrackRegister_Call_ShouldTrackRegistrationGoal(Db db, ID outcomeID, ITracker tracker, [Frozen]IAccountsSettingsService accountsSettingsService, AccountTrackerService accountTrackerService)
    {
      //Arrange
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(Substitute.For<Contact>());
      tracker.Interaction.Returns(Substitute.For<CurrentInteraction>());
      tracker.Session.Returns(Substitute.For<Session>());
      tracker.Session.CustomData.Returns(new Dictionary<string, object>());

      accountsSettingsService.GetRegistrationOutcome(Arg.Any<Item>()).Returns(outcomeID);

      db.Add(new DbItem("Item", ConfigSettings.RegistrationGoalId));
      db.Add(new DbItem("Item", ConfigSettings.LoginGoalId));

      using (db)
      using (new TrackerSwitcher(tracker))
      {
        accountTrackerService.TrackRegistration();
        tracker.CurrentPage.Received(1).Register(Arg.Is<PageEventItem>(x => x.ID == ConfigSettings.RegistrationGoalId));
        tracker.GetContactOutcomes().Should().Contain(o => o.DefinitionId == outcomeID);
      }
    }

    [Theory]
    [AutoDbData]
    public void IdentifyContact_ValidIdentifier_ShouldIdentifyContact([NoAutoProperties] AccountTrackerService trackerService, string contactIdentifier, ITracker tracker, [Substitute] Session session)
    {
      tracker.IsActive.Returns(true);
      tracker.Session.Returns(session);
      using (new TrackerSwitcher(tracker))
      {
        trackerService.IdentifyContact(contactIdentifier);
        tracker.Session.Received().Identify(contactIdentifier);
      }
    }

    [Theory]
    [AutoDbData]
    public void TrackOutcome_NullOutcomeId_ThrowException([NoAutoProperties] AccountTrackerService trackerService, ITracker tracker)
    {
      tracker.IsActive.Returns(true);
      using (new TrackerSwitcher(tracker))
      {
        trackerService.Invoking(x=>x.TrackOutcome(null)).ShouldThrow<ArgumentNullException>();
      }
    }

    [Theory]
    [AutoDbData]
    public void TrackOutcome_ValidOutcome_ShouldRegisterOutcome([Frozen]ID outcomeDefinitionId, [Frozen]IAccountsSettingsService accountsSettingsService, [NoAutoProperties] AccountTrackerService trackerService, ITracker tracker, Contact contact, CurrentInteraction interaction)
    {
      tracker.IsActive.Returns(true);
      tracker.Contact.Returns(contact);
      tracker.Interaction.Returns(interaction);
      tracker.Session.Returns(Substitute.For<Session>());
      tracker.Session.CustomData.Returns(new Dictionary<string, object>());

      using (new TrackerSwitcher(tracker))
      {
        trackerService.TrackOutcome(outcomeDefinitionId);

        tracker.GetContactOutcomes().Should().Contain(o => o.DefinitionId == outcomeDefinitionId);
      }
    }

    [Theory]
    [AutoDbData]
    public void TrackOutcome_InactiveTracker_ShouldNotTrack([Frozen]ID outcomeDefinitionId, [Frozen]IAccountsSettingsService accountsSettingsService, [NoAutoProperties] AccountTrackerService trackerService, ITracker tracker)
    {
      //Arrange
      tracker.IsActive.Returns(false);
      tracker.Session.Returns(Substitute.For<Session>());
      tracker.Session.CustomData.Returns(new Dictionary<string, object>());

      using (new TrackerSwitcher(tracker))
      {
        //Act
        trackerService.TrackOutcome(outcomeDefinitionId);

        //Assert
        tracker.GetContactOutcomes().Should().BeEmpty();
      }
      
    }

  }
}
