namespace Sitecore.Feature.Demo.Models
{
  using System;
  using System.Collections.Generic;
  using System.Data.Common;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Foundation.SitecoreExtensions.Services;

  public class ContactInformation
  {
    private readonly IContactProfileProvider contactProfileProvider;

    public ContactInformation(IContactProfileProvider contactProfileProvider)
    {
      this.contactProfileProvider = contactProfileProvider;
    }

    public int NoOfVisits => contactProfileProvider.Contact.System?.VisitCount??0;
    public string Classification => Tracker.DefinitionItems.VisitorClassifications[Tracker.Current.Contact.System.Classification].Header;
    public int EngagementValue => contactProfileProvider.Contact.System.Value;
    public Guid Id => contactProfileProvider.Contact.ContactId;

    public string Identifier => contactProfileProvider.Contact.Identifiers.Identifier;

    public string IdentificationStatus => contactProfileProvider.Contact.Identifiers.IdentificationLevel.ToString();

    public IEnumerable<string> Classifications => Tracker.DefinitionItems.VisitorClassifications.Select(c => c.Header);

    public IContactPicture Picture => contactProfileProvider.Picture?.Picture == null ? null : contactProfileProvider.Picture;
    public IContactPersonalInfo PersonalInfo => contactProfileProvider.PersonalInfo;
    public IContactAddresses Addresses => contactProfileProvider.Addresses;
    public IContactPhoneNumbers PhoneNumbers => contactProfileProvider.PhoneNumbers;
    public IContactCommunicationProfile CommunicationProfile => contactProfileProvider.CommunicationProfile;
    public IContactEmailAddresses Emails => contactProfileProvider.Emails;
    public IContactPreferences Preferences => contactProfileProvider.Preferences;
    public IEnumerable<BehaviorProfile> BehaviorProfiles => contactProfileProvider.Contact.BehaviorProfiles.Profiles.Select(x => new BehaviorProfile(x));
    public Analytics.Tracking.KeyBehaviorCache KeyBehaviorCache => contactProfileProvider.KeyBehaviorCache;
  }
}