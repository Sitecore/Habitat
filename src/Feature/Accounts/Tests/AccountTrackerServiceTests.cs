namespace Sitecore.Feature.Accounts.Tests
{
  using System;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
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
    public void TrackPageEventShouldTrackById(Database db, [Content] Item item, ITracker tracker, AccountTrackerService accountTrackerService)
    {
      using (new TrackerSwitcher(tracker))
      {
        accountTrackerService.TrackPageEvent(item.ID);
        tracker.CurrentPage.Received(1).Register(Arg.Is<PageEventItem>(x => x.ID == item.ID));
      }
    }

    [Theory, AutoDbData]
    public void TrackPageEventShouldAssertArguments(AccountTrackerService accountTrackerService)
    {
      accountTrackerService.Invoking(x => x.TrackPageEvent(null)).ShouldThrow<ArgumentNullException>();
    }

    [Theory, AutoDbData]
    public void TrackPageEventShouldAssertTracker(ID id, AccountTrackerService accountTrackerService)
    {
      accountTrackerService.Invoking(x => x.TrackPageEvent(id)).ShouldThrow<InvalidOperationException>();
    }

    [Theory, AutoDbData]
    public void TrackLoginShouldTrack(Db db, ITracker tracker, AccountTrackerService accountTrackerService)
    {
      db.Add(new DbItem("Item", ConfigSettings.LoginGoalId));

      using (db)
      using (new TrackerSwitcher(tracker))
      {
        accountTrackerService.TrackLogin();
        tracker.CurrentPage.Received(1).Register(Arg.Is<PageEventItem>(x => x.ID == ConfigSettings.LoginGoalId));
      }
    }

    [Theory, AutoDbData]
    public void TrackRegisterShouldTrack(Db db, ITracker tracker, AccountTrackerService accountTrackerService)
    {
      db.Add(new DbItem("Item", ConfigSettings.RegisterGoalId));

      using (db)
      using (new TrackerSwitcher(tracker))
      {
        accountTrackerService.TrackRegister();
        tracker.CurrentPage.Received(1).Register(Arg.Is<PageEventItem>(x => x.ID == ConfigSettings.RegisterGoalId));
      }
    }
  }
}
