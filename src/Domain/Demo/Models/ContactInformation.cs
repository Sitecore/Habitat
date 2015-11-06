using System;
using Sitecore.Analytics;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Tracking;
using KeyBehaviorCache = Sitecore.Analytics.Tracking.KeyBehaviorCache;

namespace Habitat.Demo.Models
{
  public class ContactInformation
  {
    public int NoOfVisits => Tracker.Current.Contact.System.VisitCount;
    public string Classification => Tracker.DefinitionItems.VisitorClassifications[Tracker.Current.Contact.System.Classification].Header;
    public int EngagementValue => Tracker.Current.Contact.System.Value;
    public Guid Id => Tracker.Current.Contact.ContactId;

    public IContactSystemInfo Per => GetFacet("SystemInfo");

    private IContactSystemInfo GetFacet(string facetName)
    {
      throw new NotImplementedException();
    }

    private KeyBehaviorCache GetKeyBehaviourCache()
    {
      return Tracker.Current.Contact.GetKeyBehaviorCache();
    }
  }
}