namespace Sitecore.Feature.Demo.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics.Model;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Feature.Demo.Models;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
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
               FullName = this.GetFullName(),
               IsIdentified = this.GetIsIdentified(),
               PhotoUrl = this.GetPhotoUrl(),
               Properties = this.GetProperties().ToArray(),
               Device = this.GetDevice(),
               Location = this.GetLocation()
             };
    }

    private IEnumerable<KeyValuePair<string, string>> GetProperties()
    {
      foreach (var keyValuePair in this.GetPersonalInfoProperties())
      {
        yield return keyValuePair;
      }
      foreach (var keyValuePair in this.GetAddressProperties())
      {
        yield return keyValuePair;
      }
      foreach (var keyValuePair in this.GetEmailProperties())
      {
        yield return keyValuePair;
      }
      foreach (var keyValuePair in this.GetPhoneNumberProperties())
      {
        yield return keyValuePair;
      }
      foreach (var keyValuePair in this.GetCommunicationPreferencesProperties())
      {
        yield return keyValuePair;
      }
      foreach (var keyValuePair in this.GetIdentificationProperties())
      {
        yield return keyValuePair;
      }
    }

    private IEnumerable<KeyValuePair<string, string>> GetIdentificationProperties()
    {
      if (!string.IsNullOrEmpty(this.contactProfileProvider.Contact.Identifiers.Identifier))
      {
        yield return new KeyValuePair<string, string>(DictionaryRepository.Get("/Demo/PersonalInfo/Identification", "Identification"), this.contactProfileProvider.Contact.Identifiers.Identifier);
      }
    }

    private IEnumerable<KeyValuePair<string, string>> GetCommunicationPreferencesProperties()
    {
      if (this.contactProfileProvider.CommunicationProfile.CommunicationRevoked)
      {
        yield return new KeyValuePair<string, string>(DictionaryRepository.Get("/Demo/PersonalInfo/CommunicationRevoked", "Communication Revoked"), DictionaryRepository.Get("/Demo/PersonalInfo/CommunicationRevokedTrue", "Yes"));
      }
      if (this.contactProfileProvider.CommunicationProfile.ConsentRevoked)
      {
        yield return new KeyValuePair<string, string>(DictionaryRepository.Get("/Demo/PersonalInfo/ConsentRevokes", "Consent Revoked"), DictionaryRepository.Get("/Demo/PersonalInfo/ConsentRevokedTrue", "Yes"));
      }
      if (!string.IsNullOrEmpty(this.contactProfileProvider.Preferences.Language))
      {
        yield return new KeyValuePair<string, string>(DictionaryRepository.Get("/Demo/PersonalInfo/PreferredLanguage", "Preferred Language"), this.contactProfileProvider.Preferences.Language);
      }
    }

    private IEnumerable<KeyValuePair<string, string>> GetPhoneNumberProperties()
    {
      var phoneTitle = DictionaryRepository.Get("/Demo/PersonalInfo/Phone", "Phone");
      foreach (var phoneKey in this.contactProfileProvider.PhoneNumbers.Entries.Keys)
      {
        yield return new KeyValuePair<string, string>($"{phoneTitle} ({phoneKey.Humanize()})", this.FormatPhone(this.contactProfileProvider.PhoneNumbers.Entries[phoneKey]));
      }
    }

    private string FormatPhone(IPhoneNumber phoneNumber)
    {
      var formattedPhone = "";
      if (!string.IsNullOrEmpty(phoneNumber.CountryCode))
      {
        formattedPhone += $"+{phoneNumber.CountryCode}";
      }
      formattedPhone = string.Join(" ", formattedPhone, phoneNumber.Number).Trim();
      if (!string.IsNullOrEmpty(phoneNumber.Extension))
      {
        formattedPhone = string.Join("x", formattedPhone, phoneNumber.Extension).Trim();
      }

      return formattedPhone;
    }

    private IEnumerable<KeyValuePair<string, string>> GetEmailProperties()
    {
      var emailTitle = DictionaryRepository.Get("/Demo/PersonalInfo/Email", "Email");
      foreach (var emailKey in this.contactProfileProvider.Emails.Entries.Keys)
      {
        yield return new KeyValuePair<string, string>($"{emailTitle} ({emailKey.Humanize()})", this.contactProfileProvider.Emails.Entries[emailKey].SmtpAddress);
      }
    }

    private IEnumerable<KeyValuePair<string, string>> GetAddressProperties()
    {
      var addressTitle = DictionaryRepository.Get("/Demo/PersonalInfo/Address", "Address");
      foreach (var addressKey in this.contactProfileProvider.Addresses.Entries.Keys)
      {
        yield return new KeyValuePair<string, string>($"{addressTitle} ({addressKey.Humanize()})", this.FormatAddress(this.contactProfileProvider.Addresses.Entries[addressKey]));
      }
    }

    private string FormatAddress(IAddress address)
    {
      var streetAddress = string.Join(Environment.NewLine, new List<string>
                                                           {
                                                             address.StreetLine1,
                                                             address.StreetLine2,
                                                             address.StreetLine3,
                                                             address.StreetLine4
                                                           }.Where(line => !string.IsNullOrEmpty(line))).Trim();
      var cityAddress = string.Join(", ", new List<string>
                                          {
                                            address.City,
                                            address.StateProvince,
                                            address.PostalCode
                                          }.Where(line => !string.IsNullOrEmpty(line))).Trim();
      return string.Join(Environment.NewLine, streetAddress, cityAddress, address.Country).Trim();
    }

    private IEnumerable<KeyValuePair<string, string>> GetPersonalInfoProperties()
    {
      var fullNameProperties = new[] {nameof(this.contactProfileProvider.PersonalInfo.FirstName), nameof(this.contactProfileProvider.PersonalInfo.MiddleName), nameof(this.contactProfileProvider.PersonalInfo.Suffix), nameof(this.contactProfileProvider.PersonalInfo.Surname), nameof(this.contactProfileProvider.PersonalInfo.Title)};
      foreach (var property in this.contactProfileProvider.PersonalInfo.GetType().GetProperties())
      {
        if (fullNameProperties.Contains(property.Name))
        {
          continue;
        }

        var value = property.GetValue(this.contactProfileProvider.PersonalInfo);
        if (string.IsNullOrEmpty(value?.ToString()) || property.Name.Equals("IsEmpty"))
        {
          continue;
        }
        yield return new KeyValuePair<string, string>(property.Name.Humanize(), value.ToString());
      }
    }

    private string GetPhotoUrl()
    {
      if (this.contactProfileProvider.Picture?.Picture == null)
      {
        return null;
      }
      var base64Data = Convert.ToBase64String(this.contactProfileProvider.Picture.Picture);
      return "data: image; base64," + base64Data;
    }

    private bool GetIsIdentified()
    {
      return this.contactProfileProvider.Contact.Identifiers.IdentificationLevel == ContactIdentificationLevel.Known;
    }

    private string GetFullName()
    {
      var fullName = string.Join(" ", this.contactProfileProvider.PersonalInfo.Title, this.contactProfileProvider.PersonalInfo.FirstName, this.contactProfileProvider.PersonalInfo.MiddleName, this.contactProfileProvider.PersonalInfo.Surname).Trim();
      if (!string.IsNullOrEmpty(this.contactProfileProvider.PersonalInfo.Suffix))
      {
        fullName = string.Join(", ", fullName, this.contactProfileProvider.PersonalInfo.Suffix).Trim();
      }
      return !string.IsNullOrEmpty(fullName) ? fullName : null;
    }

    private Location GetLocation()
    {
      return this.locationRepository.GetCurrent();
    }

    private Device GetDevice()
    {
      return this.deviceRepository.GetCurrent();
    }
  }
}