namespace Sitecore.Feature.Demo.Texts
{
  using Sitecore.Foundation.SitecoreExtensions.Repositories;

  public class Captions
  {
    public class VisitDetails
    {
      public static string GeoIpPostalCode => DictionaryRepository.Get("/Demo/VisitDetails/GeoIP/PostalCode", "Postal Code");
      public static string GeoIpCity => DictionaryRepository.Get("/Demo/VisitDetails/GeoIP/City", "City");
      public static string GeoIp => DictionaryRepository.Get("/Demo/VisitDetails/GeoIP", "Geo IP Location");
      public static string ThisVisit => DictionaryRepository.Get("/Demo/VisitDetails/Title", "This Visit");
      public static string PagesViewed => DictionaryRepository.Get("/Demo/VisitDetails/PagesViewed", "Pages Viewed");
      public static string EngagementValue => DictionaryRepository.Get("/Demo/VisitDetails/EngagementValue", "Engagement Value");
      public static string Patterns => DictionaryRepository.Get("/Demo/VisitDetails/Patterns", "Patterns");
      public static string Campaign => DictionaryRepository.Get("/Demo/VisitDetails/Campaign", "Campaign");
      public static string Pages => DictionaryRepository.Get("/Demo/VisitDetails/Pages", "Pages");

      public static string Goals => DictionaryRepository.Get("/Demo/VisitDetails/Goals", "Goals");
      public static string Engagement => DictionaryRepository.Get("/Demo/VisitDetails/Engagement", "Engagement");
      public static string EndVisit => DictionaryRepository.Get("/Demo/VisitDetails/EndVisit", "End Visit");
      public static string EndVisitDescription => DictionaryRepository.Get("/Demo/VisitDetails/EndVisit/Description", "End the current visit and submits it to the Sitecore Experience Database");

      public class Device
      {
        public static string OperatingSystem => DictionaryRepository.Get("/Demo/VisitDetails/Device/OS", "Operating System");
        public static string Title => DictionaryRepository.Get("/Demo/VisitDetails/Device", "Device");
        public static string Type => DictionaryRepository.Get("/Demo/VisitDetails/Device/Type", "Device Type");
        public static string Vendor => DictionaryRepository.Get("/Demo/VisitDetails/Device/Vendor", "Vendor");
        public static string Browser => DictionaryRepository.Get("/Demo/VisitDetails/Device/Browser", "Browser");
        public static string DisplayHeight => DictionaryRepository.Get("/Demo/VisitDetails/Device/DisplayHeight", "Display Height");
        public static string DisplayWidth => DictionaryRepository.Get("/Demo/VisitDetails/Device/DisplayWidth", "Display Width");
      }
    }

    public class ContactDetails
    {
      public static string Title => DictionaryRepository.Get("/Demo/ContactDetails/Title", "Contact");
      public static string NumberOfVisits => DictionaryRepository.Get("/Demo/ContactDetails/NoOfVisits", "Number of Visits");
      public static string EngagementValue => DictionaryRepository.Get("/Demo/ContactDetails/EngagementValue", "Engagement Value");
      public static string Identification => DictionaryRepository.Get("/Demo/ContactDetails/Identification", "Identification");
      public static string ContactId => DictionaryRepository.Get("/Demo/ContactDetails/ContactID", "Contact ID");
      public static string Identifier => DictionaryRepository.Get("/Demo/ContactDetails/Identifier", "Identifier");
      public static string IdentificationStatus => DictionaryRepository.Get("/Demo/ContactDetails/IdentificationStatus", "Identification status");

      public static string PersonalData => DictionaryRepository.Get("/Demo/ContactDetails/PersonalData", "Personal Data");
      public static string Picture => DictionaryRepository.Get("/Demo/ContactDetails/Picture", "Picture");
      public static string Addresses => DictionaryRepository.Get("/Demo/ContactDetails/Addresses", "Addresses");
      public static string PhoneNumbers => DictionaryRepository.Get("/Demo/ContactDetails/PhoneNumbers", "Phone Numbers");

      public static string Communication => DictionaryRepository.Get("/Demo/ContactDetails/Communication", "Communication");
      public static string CommunicationRevoked => DictionaryRepository.Get("/Demo/ContactDetails/CommunicationRevoked", "Communication Revoked");
      public static string ConsentRevoked => DictionaryRepository.Get("/Demo/ContactDetails/ConsentRevoked", "Consent Revoked");
      public static string EmailAddresses => DictionaryRepository.Get("/Demo/ContactDetails/EmailAddresses", "Email Addresses");
      public static string PreferenceLanguage => DictionaryRepository.Get("/Demo/ContactDetails/PreferenceLanguage", "Preference Language");

      public static string BehaviorProfiles => DictionaryRepository.Get("/Demo/ContactDetails/BehaviorProfiles", "Behavior Profiles");
      public static string TimesScored => DictionaryRepository.Get("/Demo/ContactDetails/NumberOfTimesScored", "Number Of Times Scored");
      public static string Scores => DictionaryRepository.Get("/Demo/ContactDetails/Scores", "Scores");

      public static string KeyBehaviorCache => DictionaryRepository.Get("/Demo/ContactDetails/KeyBehaviorCache", "Key Behavior Cache");
      public static string Campaigns => DictionaryRepository.Get("/Demo/ContactDetails/Campaigns", "Campaigns");
      public static string Channels => DictionaryRepository.Get("/Demo/ContactDetails/Channels", "Channels");
      public static string Goals => DictionaryRepository.Get("/Demo/ContactDetails/Goals", "Goals");
      public static string CustomValues => DictionaryRepository.Get("/Demo/ContactDetails/CustomValues", "Custom Values");
      public static string Outcomes => DictionaryRepository.Get("/Demo/ContactDetails/Outcomes", "Outcomes");
      public static string PageEvents => DictionaryRepository.Get("/Demo/ContactDetails/PageEvents", "Page Events");
      public static string Venues => DictionaryRepository.Get("/Demo/ContactDetails/Venues", "Venues");

    }
  }
}