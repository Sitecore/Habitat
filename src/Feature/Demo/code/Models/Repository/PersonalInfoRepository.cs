namespace Sitecore.Feature.Demo.Models.Repository
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics.Model;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Foundation.Dictionary.Repositories;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Foundation.SitecoreExtensions.Services;

  public class PersonalInfoRepository
  {
    private readonly LocationRepository locationRepository = new LocationRepository();
    private readonly DeviceRepository deviceRepository = new DeviceRepository();
    private readonly IContactProfileProvider contactProfileProvider;

    public PersonalInfoRepository(IContactProfileProvider contactProfileProvider)
    {
      this.contactProfileProvider = contactProfileProvider;
    }

    public PersonalInfo Get()
    {
      return new PersonalInfo
             {
               FullName = GetFullName(),
               IsIdentified = GetIsIdentified(),
               PhotoUrl = GetPhotoUrl(),
               Properties = GetProperties().ToArray(),
               Device = GetDevice(),
               Location = GetLocation()
             };
    }

    private IEnumerable<KeyValuePair<string, string>> GetProperties()
    {
      foreach (var keyValuePair in GetPersonalInfoProperties())
      {
        yield return keyValuePair;
      }
      foreach (var keyValuePair in GetAddressProperties())
      {
        yield return keyValuePair;
      }
      foreach (var keyValuePair in GetEmailProperties())
      {
        yield return keyValuePair;
      }
      foreach (var keyValuePair in GetPhoneNumberProperties())
      {
        yield return keyValuePair;
      }
      foreach (var keyValuePair in GetCommunicationPreferencesProperties())
      {
        yield return keyValuePair;
      }
      foreach (var keyValuePair in GetIdentificationProperties())
      {
        yield return keyValuePair;
      }
    }

    private IEnumerable<KeyValuePair<string, string>> GetIdentificationProperties()
    {
      if (!string.IsNullOrEmpty(contactProfileProvider.Contact.Identifiers.Identifier))
      {
        yield return new KeyValuePair<string, string>(DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Identification", "Identification"), contactProfileProvider.Contact.Identifiers.Identifier);
      }
    }

    private IEnumerable<KeyValuePair<string, string>> GetCommunicationPreferencesProperties()
    {
      if (contactProfileProvider.CommunicationProfile.CommunicationRevoked)
      {
        yield return new KeyValuePair<string, string>(DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Communication Revoked", "Communication Revoked"), DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Communication Revoked True", "Yes"));
      }
      if (contactProfileProvider.CommunicationProfile.ConsentRevoked)
      {
        yield return new KeyValuePair<string, string>(DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Consent Revoked", "Consent Revoked"), DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Consent Revoked True", "Yes"));
      }
      if (!string.IsNullOrEmpty(contactProfileProvider.Preferences.Language))
      {
        yield return new KeyValuePair<string, string>(DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Preferred Language", "Preferred Language"), contactProfileProvider.Preferences.Language);
      }
    }

    private IEnumerable<KeyValuePair<string, string>> GetPhoneNumberProperties()
    {
      var phoneTitle = DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Phone", "Phone");
      foreach (var phoneKey in contactProfileProvider.PhoneNumbers.Entries.Keys)
      {
        yield return new KeyValuePair<string, string>($"{phoneTitle} ({phoneKey.Humanize()})", FormatPhone(contactProfileProvider.PhoneNumbers.Entries[phoneKey]));
      }
    }

    private string FormatPhone(IPhoneNumber phoneNumber)
    {
      var formattedPhone = "";
      if (!string.IsNullOrEmpty(phoneNumber.CountryCode))
      {
        formattedPhone += $"+{phoneNumber.CountryCode}";
      }
      formattedPhone += string.Join(" ", formattedPhone, phoneNumber.Number).Trim();
      if (!string.IsNullOrEmpty(phoneNumber.Extension))
      {
        formattedPhone += string.Join("p", formattedPhone, phoneNumber.Extension).Trim();
      }

      return formattedPhone;
    }

    private IEnumerable<KeyValuePair<string, string>> GetEmailProperties()
    {
      var emailTitle = DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Email", "Email");
      foreach (var emailKey in contactProfileProvider.Emails.Entries.Keys)
      {
        yield return new KeyValuePair<string, string>($"{emailTitle} ({emailKey.Humanize()})", contactProfileProvider.Emails.Entries[emailKey].SmtpAddress);
      }
    }

    private IEnumerable<KeyValuePair<string, string>> GetAddressProperties()
    {
      var addressTitle = DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Address", "Address");
      foreach (var addressKey in contactProfileProvider.Addresses.Entries.Keys)
      {
        yield return new KeyValuePair<string, string>($"{addressTitle} ({addressKey.Humanize()})", FormatAddress(contactProfileProvider.Addresses.Entries[addressKey]));
      }
    }

    private string FormatAddress(IAddress address)
    {
      var streetAddress = string.Join(Environment.NewLine, new
                                                           {
                                                             address.StreetLine1,
                                                             address.StreetLine2,
                                                             address.StreetLine3,
                                                             address.StreetLine4
                                                           }).Trim();
      var cityAddress = string.Join(", ", new
                                          {
                                            address.City,
                                            address.StateProvince,
                                            address.PostalCode
                                          }).Trim();
      return string.Join(Environment.NewLine, streetAddress, cityAddress, address.Country).Trim();
    }

    private IEnumerable<KeyValuePair<string, string>> GetPersonalInfoProperties()
    {
      var fullNameProperties = new[] {nameof(contactProfileProvider.PersonalInfo.FirstName), nameof(contactProfileProvider.PersonalInfo.MiddleName), nameof(contactProfileProvider.PersonalInfo.Suffix), nameof(contactProfileProvider.PersonalInfo.Surname), nameof(contactProfileProvider.PersonalInfo.Title)};
      foreach (var property in contactProfileProvider.PersonalInfo.GetType().GetProperties())
      {
        if (fullNameProperties.Contains(property.Name))
        {
          continue;
        }

        var value = property.GetValue(contactProfileProvider.PersonalInfo);
        if (string.IsNullOrEmpty(value?.ToString()) || property.Name.Equals("IsEmpty"))
        {
          continue;
        }
        yield return new KeyValuePair<string, string>(property.Name.Humanize(), value.ToString());
      }
    }

    private string GetPhotoUrl()
    {
      if (contactProfileProvider.Picture?.Picture == null)
      {
        return null;
      }
      var base64Data = Convert.ToBase64String(contactProfileProvider.Picture.Picture);
      return "data: image; base64," + base64Data;
    }

    private bool GetIsIdentified()
    {
      return contactProfileProvider.Contact.Identifiers.IdentificationLevel == ContactIdentificationLevel.Known;
    }

    private string GetFullName()
    {
      var fullName = string.Join(" ", contactProfileProvider.PersonalInfo.Title, contactProfileProvider.PersonalInfo.FirstName, contactProfileProvider.PersonalInfo.MiddleName, contactProfileProvider.PersonalInfo.Surname).Trim();
      if (!string.IsNullOrEmpty(contactProfileProvider.PersonalInfo.Suffix))
      {
        fullName = string.Join(", ", fullName, contactProfileProvider.PersonalInfo.Suffix).Trim();
      }
      return !string.IsNullOrEmpty(fullName) ? fullName : null;
    }

    private Location GetLocation()
    {
      return locationRepository.GetCurrent();
    }

    private Device GetDevice()
    {
      return deviceRepository.GetCurrent();
    }
  }
}