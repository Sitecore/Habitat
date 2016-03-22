using Sitecore.Data;

namespace Sitecore.Foundation.SitecoreExtensions.Services
{
  public interface ITrackerService
  {
    void IdentifyContact(string identifier);
    void TrackOutcome(ID definitionId);
    void TrackPageEvent(ID pageEventItemId);
    bool IsActive { get; }
  }
}