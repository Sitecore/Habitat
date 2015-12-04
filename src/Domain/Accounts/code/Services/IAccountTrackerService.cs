using Sitecore.Data;

namespace Habitat.Accounts.Services
{
  public interface IAccountTrackerService
  {
    void TrackLogin();
    void TrackPageEvent(ID pageEventItemId);
    void TrackRegister();

    void IdentifyContact(string identifier);
  }
}