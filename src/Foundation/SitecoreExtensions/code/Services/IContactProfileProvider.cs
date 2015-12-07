namespace Sitecore.Foundation.SitecoreExtensions.Services
{
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Analytics.Tracking;

  public interface IContactProfileProvider
  {
    IContactPersonalInfo PersonalInfo { get; }

    IContactAddresses Adresses { get; }

    IContactEmailAddresses Emails { get; }

    IContactCommunicationProfile CommunicationProfile { get; }

    IContactPhoneNumbers PhoneNumbers { get; }

    Contact Flush();

    Contact Contact{ get; }
  }
}
