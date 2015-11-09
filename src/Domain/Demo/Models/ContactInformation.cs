using System;
using System.Collections.Generic;
using System.Linq;
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
    public IEnumerable<string> Classifications
    {
      get { return Tracker.DefinitionItems.VisitorClassifications.Select(c => c.Header); }
    }
  }
}