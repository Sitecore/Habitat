namespace Sitecore.Feature.Accounts.Services
{
  using Sitecore.Data;

  public interface IAccountTrackerService
  {
    void TrackPageEvent(ID pageEventItemId);
    void TrackRegister();

    void IdentifyContact(string identifier);
    void TrackOutcome(ID definitionId);
    void TrackRegisterOutcome();
    void TrackLogin(string identifier);
  }
}