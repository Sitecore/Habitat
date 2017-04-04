namespace Sitecore.Feature.Accounts.Services
{
  public interface IAccountTrackerService
  {
    void TrackRegistration();
    void TrackRegistrationOutcome();
    void TrackLoginAndIdentifyContact(string identifier);
  }
}