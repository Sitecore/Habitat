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
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Xunit;

  public class AccountTrackerServiceTests
  {
    [Theory, AutoDbData]
    public void TrackLogin_Call_ShouldTrackLoginGoal(string identifier, Db db, [Frozen]ITrackerService trackerService, [Greedy]AccountTrackerService accountTrackerService)
    {
      //Arrange
      db.Add(new DbItem("Item", ConfigSettings.LoginGoalId));

      //Act
      accountTrackerService.TrackLogin(identifier);

      //Assert
      trackerService.Received().TrackPageEvent(Arg.Is<ID>(ConfigSettings.LoginGoalId));
    }

    [Theory, AutoDbData]
    public void TrackRegister_Call_ShouldTrackRegistrationGoal(Db db, ID outcomeID, ITracker tracker, [Frozen]IAccountsSettingsService accountsSettingsService, [Frozen]ITrackerService trackerService, [Greedy]AccountTrackerService accountTrackerService)
    {
      // Arrange
      accountsSettingsService.GetRegistrationOutcome(Arg.Any<Item>()).Returns(outcomeID);

      db.Add(new DbItem("Item", ConfigSettings.RegistrationGoalId));
      db.Add(new DbItem("Item", ConfigSettings.LoginGoalId));

      //Act
      accountTrackerService.TrackRegistration();

      //Assert
      trackerService.TrackPageEvent(ConfigSettings.RegistrationGoalId);
      trackerService.TrackOutcome(outcomeID);
    }
  }
}
