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
  using Sitecore.Analytics.Tracking;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Accounts.Services;
  using Sitecore.Feature.Accounts.Tests.Extensions;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class AccountTrackerServiceTests
  {
    [Theory, AutoDbData]
    public void TrackLogin_Call_ShouldTrackLoginGoal(string source, string identifier, [Frozen]ITrackerService trackerService, [Greedy]AccountTrackerService accountTrackerService)
    {
      //Act
      accountTrackerService.TrackLoginAndIdentifyContact(source, identifier);

      //Assert
      trackerService.Received(1).TrackGoal(AccountTrackerService.LoginGoalId, source);
      trackerService.Received(1).IdentifyContact(source, identifier);
    }

    [Theory, AutoDbData]
    public void TrackRegister_Call_ShouldTrackRegistrationGoal(ID outcomeID, ITracker tracker, [Frozen]IAccountsSettingsService accountsSettingsService, [Frozen]ITrackerService trackerService, [Greedy]AccountTrackerService accountTrackerService)
    {
      // Arrange
      accountsSettingsService.GetRegistrationOutcome(Arg.Any<Item>()).Returns(outcomeID.Guid);

      //Act
      accountTrackerService.TrackRegistration();

      //Assert
      trackerService.Received(1).TrackGoal(AccountTrackerService.RegistrationGoalId);
      trackerService.Received(1).TrackOutcome(outcomeID.Guid);
    }
  }
}
