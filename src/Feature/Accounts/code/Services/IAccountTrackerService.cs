namespace Sitecore.Feature.Accounts.Services
{
  using Sitecore.Data;

  public interface IAccountTrackerService
  {
    void TrackRegistration();
    void TrackRegistrationOutcome();
    void TrackLogin(string identifier);
  }
}