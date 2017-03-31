namespace Sitecore.Feature.Accounts.Services
{
  using Sitecore.Configuration;
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

    public virtual void TrackLoginAndIdentifyContact(string identifier)
    {
      this.trackerService.TrackPageEvent(LoginGoalId);
      this.trackerService.IdentifyContact(identifier);
    }

    public virtual void TrackRegistration()
    {
      this.trackerService.TrackPageEvent(RegistrationGoalId);
      this.TrackRegistrationOutcome();
    }

    public void TrackRegistrationOutcome()
    {
      var outcomeId = this.accountsSettingsService.GetRegistrationOutcome(Context.Item);
      if (outcomeId != (ID)null && !outcomeId.IsNull)
      {
        this.trackerService.TrackOutcome(outcomeId);
      }
    }

    public static ID LoginGoalId => new ID(Settings.GetSetting("Sitecore.Feature.Accounts.LoginGoalId", "{66722F52-2D13-4DCC-90FC-EA7117CF2298}"));
    public static ID RegistrationGoalId => new ID(Settings.GetSetting("Sitecore.Feature.Accounts.RegistrationGoalId", "{8FFB183B-DA1A-4C74-8F3A-9729E9FCFF6A}"));
  }
}