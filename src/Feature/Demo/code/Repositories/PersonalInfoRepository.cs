namespace Sitecore.Feature.Demo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Sitecore.Analytics.Model;
    using Sitecore.Analytics.Model.Entities;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Foundation.Accounts.Providers;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.XConnect.Collection.Model;

    [Service(typeof(IPersonalInfoRepository))]
    public class PersonalInfoRepository : IPersonalInfoRepository
    {
        private readonly LocationRepository locationRepository;
        private readonly DeviceRepository deviceRepository;
        private readonly IContactFacetsProvider contactFacetsProvider;

        public PersonalInfoRepository(IContactFacetsProvider contactFacetsProvider, LocationRepository locationRepository, DeviceRepository deviceRepository)
        {
            this.contactFacetsProvider = contactFacetsProvider;
            this.locationRepository = locationRepository;
            this.deviceRepository = deviceRepository;
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
            foreach (var keyValuePair in this.GetTagsProperties())
            {
                yield return keyValuePair;
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetIdentificationProperties()
        {
            var identifiers = this.contactFacetsProvider?.Contact?.Identifiers;
            if (identifiers == null)
                yield break;

            foreach (var identifier in identifiers)
            {
                switch (identifier.Type)
                {
                    case ContactIdentificationLevel.Anonymous:
                        yield return new KeyValuePair<string, string>(string.Format(DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Anonymous ID", "Anonymous ID ({0})"), identifier.Source), identifier.Identifier);
                        break;
                    case ContactIdentificationLevel.Known:
                        yield return new KeyValuePair<string, string>(string.Format(DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Identification", "Known ID ({0})"), identifier.Source), identifier.Identifier);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetCommunicationPreferencesProperties()
        {
            if (this.contactFacetsProvider?.CommunicationProfile == null)
                yield break;

            if (!this.contactFacetsProvider.IsKnown)
                yield break;

            yield return new KeyValuePair<string, string>(DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Do Not Market", "Do not market"), this.contactFacetsProvider.CommunicationProfile.DoNotMarket ? DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Yes", "Yes") : DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/No", "No"));
            yield return new KeyValuePair<string, string>(DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Consent Revoked", "Consent revoked"), this.contactFacetsProvider.CommunicationProfile.ConsentRevoked ? DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Yes", "Yes") : DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/No", "No"));
            yield return new KeyValuePair<string, string>(DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Executed Right To Be Forgotten", "Executed right to be forgotten"), this.contactFacetsProvider.CommunicationProfile.ExecutedRightToBeForgotten ? DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Yes", "Yes") : DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/No", "No"));
            if (!string.IsNullOrEmpty(this.contactFacetsProvider.PersonalInfo.PreferredLanguage))
            {
                yield return new KeyValuePair<string, string>(DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Preferred Language", "Preferred Language"), this.contactFacetsProvider.PersonalInfo.PreferredLanguage);
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetPhoneNumberProperties()
        {
            if (this.contactFacetsProvider.PhoneNumbers == null)
                yield break;

            var phoneTitle = DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Phone", "Phone");
            yield return new KeyValuePair<string, string>($"{phoneTitle}", this.FormatPhone(this.contactFacetsProvider.PhoneNumbers.PreferredPhoneNumber));
            foreach (var phoneKey in this.contactFacetsProvider.PhoneNumbers.Others.Keys)
            {
                yield return new KeyValuePair<string, string>($"{phoneTitle} ({phoneKey.Humanize()})", this.FormatPhone(this.contactFacetsProvider.PhoneNumbers.Others[phoneKey]));
            }
        }

        private string FormatPhone(PhoneNumber phoneNumber)
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
            if (this.contactFacetsProvider?.Emails == null)
                yield break;

            var emailTitle = DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Email", "Email");
            yield return new KeyValuePair<string, string>($"{emailTitle}", this.contactFacetsProvider.Emails.PreferredEmail.SmtpAddress);
            foreach (var emailKey in this.contactFacetsProvider.Emails.Others.Keys)
            {
                yield return new KeyValuePair<string, string>($"{emailTitle} ({emailKey.Humanize()})", this.contactFacetsProvider.Emails.Others[emailKey].SmtpAddress);
            }
        }

        private IEnumerable<KeyValuePair<string, string>> GetAddressProperties()
        {
            if (this.contactFacetsProvider?.Addresses == null)
                yield break;

            var addressTitle = DictionaryPhraseRepository.Current.Get("/Demo/Personal Info/Address", "Address");
            yield return new KeyValuePair<string, string>($"{addressTitle})", this.FormatAddress(this.contactFacetsProvider.Addresses.PreferredAddress));
            foreach (var addressKey in this.contactFacetsProvider.Addresses.Others.Keys)
            {
                yield return new KeyValuePair<string, string>($"{addressTitle} ({addressKey.Humanize()})", this.FormatAddress(this.contactFacetsProvider.Addresses.Others[addressKey]));
            }
        }

        private string FormatAddress(Address address)
        {
            var streetAddress = string.Join(Environment.NewLine, new List<string>
                                                                 {
                                                                     address.AddressLine1,
                                                                     address.AddressLine2,
                                                                     address.AddressLine3,
                                                                     address.AddressLine4
                                                                 }.Where(line => !string.IsNullOrEmpty(line))).Trim();
            var cityAddress = string.Join(", ", new List<string>
                                                {
                                                    address.City,
                                                    address.StateOrProvince,
                                                    address.PostalCode
                                                }.Where(line => !string.IsNullOrEmpty(line))).Trim();
            return string.Join(Environment.NewLine, streetAddress, cityAddress, address.CountryCode).Trim();
        }

        private IEnumerable<KeyValuePair<string, string>> GetPersonalInfoProperties()
        {
            if (this.contactFacetsProvider?.PersonalInfo == null)
                yield break;

            var fullNameProperties = new[] {nameof(this.contactFacetsProvider.PersonalInfo.FirstName), nameof(this.contactFacetsProvider.PersonalInfo.MiddleName), nameof(this.contactFacetsProvider.PersonalInfo.Suffix), nameof(this.contactFacetsProvider.PersonalInfo.LastName), nameof(this.contactFacetsProvider.PersonalInfo.Title)};
            foreach (var property in this.contactFacetsProvider.PersonalInfo.GetType().GetProperties(BindingFlags.DeclaredOnly))
            {
                if (fullNameProperties.Contains(property.Name))
                {
                    continue;
                }

                var value = property.GetValue(this.contactFacetsProvider.PersonalInfo);
                if (string.IsNullOrEmpty(value?.ToString()) || property.Name.Equals("IsEmpty"))
                {
                    continue;
                }
                yield return new KeyValuePair<string, string>(property.Name.Humanize(), value.ToString());
            }
        }

        
        private IEnumerable<KeyValuePair<string, string>> GetTagsProperties()
        {
            return this.contactFacetsProvider?.Contact?.Tags?.GetAll().Select(this.GetTagValue) ?? Enumerable.Empty<KeyValuePair<string, string>>();
        }

        private KeyValuePair<string, string> GetTagValue(KeyValuePair<string, ITag> tag)
        {
            return new KeyValuePair<string, string>(tag.Key, this.GetTagValue(tag.Value));
        }

        private string GetTagValue(ITag value)
        {
            var strings = value?.Values.Select(v => v.Value).ToArray();
            return strings == null ? string.Empty : string.Join(", ", strings);
        }


        private string GetPhotoUrl()
        {
            if (this.contactFacetsProvider?.Picture?.Picture == null)
            {
                return null;
            }
            var base64Data = Convert.ToBase64String(this.contactFacetsProvider.Picture.Picture);
            return "data: image; base64," + base64Data;
        }

        private bool GetIsIdentified()
        {
            return this.contactFacetsProvider?.Contact?.Identifiers.Any(id => id.Type == ContactIdentificationLevel.Known) ?? false;
        }

        private string GetFullName()
        {
            if (this.contactFacetsProvider?.PersonalInfo == null)
            {
                return null;
            }

            var fullName = string.Join(" ", this.contactFacetsProvider.PersonalInfo.Title, this.contactFacetsProvider.PersonalInfo.FirstName, this.contactFacetsProvider.PersonalInfo.MiddleName, this.contactFacetsProvider.PersonalInfo.LastName).Trim();
            if (!string.IsNullOrEmpty(this.contactFacetsProvider.PersonalInfo.Suffix))
            {
                fullName = string.Join(", ", fullName, this.contactFacetsProvider.PersonalInfo.Suffix).Trim();
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