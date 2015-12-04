namespace Sitecore.Feature.Accounts.Services
{
  using Sitecore.Data;

  public interface IAccountTrackerService
  {
    void TrackLogin();
    void TrackPageEvent(ID pageEventItemId);
    void TrackRegister();

    void IdentifyContact(string identifier);
  }
}