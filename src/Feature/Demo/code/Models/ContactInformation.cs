namespace Sitecore.Feature.Demo.Models
{
  using System;
  using System.Collections.Generic;
  using System.Data.Common;
  using System.Linq;
  using Sitecore.Analytics;

  public class ContactInformation
  {
    public int NoOfVisits => Tracker.Current.Contact.System.VisitCount;
    public string Classification => Tracker.DefinitionItems.VisitorClassifications[Tracker.Current.Contact.System.Classification].Header;
    public int EngagementValue => Tracker.Current.Contact.System.Value;
    public Guid Id => Tracker.Current.Contact.ContactId;

    public string Identifier => Tracker.Current.Contact.Identifiers.Identifier;

    public string IdentificationStatus => Tracker.Current.Contact.Identifiers.IdentificationLevel.ToString();

    public IEnumerable<string> Classifications
    {
      get
      {
        return Tracker.DefinitionItems.VisitorClassifications.Select(c => c.Header);
      }
    }
  }
}