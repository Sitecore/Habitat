namespace Sitecore.Foundation.SitecoreExtensions.Services
{
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Analytics.Tracking;

  public interface IContactProfileProvider
  {
    IContactPersonalInfo PersonalInfo { get; }

    IContactAddresses Addresses { get; }

    IContactEmailAddresses Emails { get; }

    IContactCommunicationProfile CommunicationProfile { get; }

    IContactPhoneNumbers PhoneNumbers { get; }

    Contact Flush();

    Contact Contact { get; }
    IContactPicture Picture { get; }
    IContactPreferences Preferences { get; }
    Analytics.Tracking.KeyBehaviorCache KeyBehaviorCache { get; }
  }
}