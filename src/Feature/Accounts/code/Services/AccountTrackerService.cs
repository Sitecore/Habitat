namespace Sitecore.Feature.Accounts.Services
{
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Data;
  using Sitecore.Diagnostics;

  public class AccountTrackerService : IAccountTrackerService
  {
    public virtual void TrackLogin()
    {
      this.TrackPageEvent(ConfigSettings.LoginGoalId);
    }

    public virtual void TrackRegister()
    {
      this.TrackPageEvent(ConfigSettings.RegisterGoalId);
    }

    public virtual void TrackPageEvent(ID pageEventItemId)
    {
      Assert.ArgumentNotNull(pageEventItemId, nameof(pageEventItemId));
      var item = Sitecore.Context.Database.GetItem(pageEventItemId);
      Assert.IsNotNull(item, $"Cannot find page event: {pageEventItemId}");
      Assert.IsNotNull(Tracker.Current, "Analytics tracker isn't initialized");

      var pageEventItem = new PageEventItem(item);
      Tracker.Current.CurrentPage.Register(pageEventItem);
    }
  }
}