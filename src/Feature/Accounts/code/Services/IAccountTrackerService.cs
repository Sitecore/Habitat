namespace Sitecore.Feature.Accounts.Services
{
  using Sitecore.Data;

  public interface IAccountTrackerService
  {
    void TrackPageEvent(ID pageEventItemId);
    void TrackRegistration();
    void IdentifyContact(string identifier);
    void TrackOutcome(ID definitionId);
    void TrackRegistrationOutcome();
    void TrackLogin(string identifier);
  }
}