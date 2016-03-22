namespace Sitecore.Foundation.SitecoreExtensions.Services
{
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Analytics.Outcome.Extensions;
  using Sitecore.Analytics.Outcome.Model;
  using Sitecore.Data;
  using Sitecore.Diagnostics;
  using Sitecore.Exceptions;

  public class TrackerService : ITrackerService
  {
    public bool IsActive => Tracker.Current != null && Tracker.Current.IsActive;

    public virtual void TrackPageEvent(ID pageEventItemId)
    {
      Assert.ArgumentNotNull(pageEventItemId, nameof(pageEventItemId));
      var item = Sitecore.Context.Database.GetItem(pageEventItemId);
      Assert.IsNotNull(item, $"Cannot find page event: {pageEventItemId}");
      if (this.IsActive)
      {
        var pageEventItem = new PageEventItem(item);
        Tracker.Current.CurrentPage.Register(pageEventItem);
      }
    }

    public void TrackOutcome(ID definitionId)
    {
      Assert.ArgumentNotNull(definitionId, nameof(definitionId));

      if (this.IsActive && Tracker.Current.Contact != null)
      {
        var outcomeId = new ID();
        var contactId = new ID(Tracker.Current.Contact.ContactId);

        var outcome = new ContactOutcome(outcomeId, definitionId, contactId);
        Tracker.Current.RegisterContactOutcome(outcome);
      }
    }

    public void IdentifyContact(string identifier)
    {
      try
      {
        if (this.IsActive)
        {
          Tracker.Current.Session.Identify(identifier);
        }
      }
      catch (ItemNotFoundException ex)
      {
        //Error can happen if previous user profile has been deleted
        Log.Error($"Could not identify the user '{identifier}'", ex, this);
      }
    }
  }
}