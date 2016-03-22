namespace Sitecore.Feature.Accounts.Services
{
  using Sitecore.Data;
  using Sitecore.Foundation.SitecoreExtensions.Services;

  public class AccountTrackerService : IAccountTrackerService
  {
    private readonly IAccountsSettingsService accountsSettingsService;
    private readonly ITrackerService trackerService;

    public AccountTrackerService(IAccountsSettingsService accountsSettingsService, ITrackerService trackerService)
    {
      this.accountsSettingsService = accountsSettingsService;
      this.trackerService = trackerService;
    }

    public virtual void TrackLogin(string identifier)
    {
      this.trackerService.TrackPageEvent(ConfigSettings.LoginGoalId);
      this.trackerService.IdentifyContact(identifier);
    }

    public virtual void TrackRegistration()
    {
      this.trackerService.TrackPageEvent(ConfigSettings.RegistrationGoalId);
      this.TrackRegistrationOutcome();
    }

    public void TrackRegistrationOutcome()
    {
      var outcomeId = accountsSettingsService.GetRegistrationOutcome(Context.Item);
      if (outcomeId != (ID)null && !outcomeId.IsNull)
      {
        this.trackerService.TrackOutcome(outcomeId);
      }
    }
  }
}