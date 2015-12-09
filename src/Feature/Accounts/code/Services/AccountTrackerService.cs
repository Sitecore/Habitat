namespace Sitecore.Feature.Accounts.Services
{
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Analytics.Outcome.Extensions;
  using Sitecore.Analytics.Outcome.Model;
  using Sitecore.Data;
  using Sitecore.Diagnostics;

  public class AccountTrackerService : IAccountTrackerService
  {
    private readonly IAccountsSettingsService accountsSettingsService;

    public AccountTrackerService(IAccountsSettingsService accountsSettingsService)
    {
      this.accountsSettingsService = accountsSettingsService;
    }

    public virtual void TrackLogin(string identifier)
    {
      this.TrackPageEvent(ConfigSettings.LoginGoalId);
      this.IdentifyContact(identifier);
    }

    public virtual void TrackRegister()
    {
      this.TrackPageEvent(ConfigSettings.RegisterGoalId);
      this.TrackRegisterOutcome();
    }

    public void IdentifyContact(string identifier)
    {
      if (Tracker.Current != null && Tracker.Current.IsActive)
      {
        Tracker.Current.Session.Identify(identifier);
      }
    }

    public void TrackRegisterOutcome()
    {
      var outcomeId = accountsSettingsService.GetRegisterOutcome(Context.Item);
      if (outcomeId != (ID)null && !outcomeId.IsNull)
      {
        this.TrackOutcome(outcomeId);
      }
    }

    public void TrackOutcome(ID definitionId)
    {
      Assert.ArgumentNotNull(definitionId, nameof(definitionId));

      if (Tracker.Current != null && Tracker.Current.IsActive && Tracker.Current.Contact != null)
      {
        var outcomeId = new ID();
        var contactId= new ID(Tracker.Current.Contact.ContactId);

        var outcome = new ContactOutcome(outcomeId, definitionId, contactId);
        Tracker.Current.RegisterContactOutcome(outcome);
      }
    }

    public virtual void TrackPageEvent(ID pageEventItemId)
    {
      Assert.ArgumentNotNull(pageEventItemId, nameof(pageEventItemId));
      var item = Sitecore.Context.Database.GetItem(pageEventItemId);
      Assert.IsNotNull(item, $"Cannot find page event: {pageEventItemId}");
      if (Tracker.Current != null && Tracker.Current.IsActive)
      {
        var pageEventItem = new PageEventItem(item);
        Tracker.Current.CurrentPage.Register(pageEventItem);
      }
    }
  }
}