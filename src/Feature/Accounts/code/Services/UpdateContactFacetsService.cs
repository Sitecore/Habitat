namespace Sitecore.Feature.Accounts.Services
{
    using System;
    using System.Linq;
    using System.Net;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Configuration;
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.Accounts.Providers;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Security;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Client;
    using Sitecore.XConnect.Client.Configuration;
    using Sitecore.XConnect.Collection.Model;

    [Service(typeof(IUpdateContactFacetsService))]
    public class UpdateContactFacetsService : IUpdateContactFacetsService
    {
        private readonly IContactFacetsProvider contactFacetsProvider;
        private readonly ContactManager contactManager;
        private readonly string[] facetsToUpdate = {PersonalInformation.DefaultFacetKey, AddressList.DefaultFacetKey, EmailAddressList.DefaultFacetKey, ConsentInformation.DefaultFacetKey, PhoneNumberList.DefaultFacetKey, Avatar.DefaultFacetKey};

        public UpdateContactFacetsService(IContactFacetsProvider contactFacetsProvider)
        {
            this.contactFacetsProvider = contactFacetsProvider;
            this.contactManager = Factory.CreateObject("tracking/contactManager", true) as ContactManager;
        }

        public void UpdateContactFacets(UserProfile profile)
        {
            var id = this.GetContactId();
            if (id == null)
            {
                return;
            }

            var contactReference = new IdentifiedContactReference(id.Source, id.Identifier);

            using (var client = SitecoreXConnectClientConfiguration.GetClient())
            {
                try
                {
                    var contact = client.Get(contactReference, new ContactExpandOptions(this.facetsToUpdate));
                    if (contact == null)
                    {
                        return;
                    }
                    var changed = false;
                    changed |= SetPersonalInfo(profile, contact, client);
                    changed |= this.SetPhone(profile, contact, client);
                    changed |= this.SetEmail(profile, contact, client);
                    changed |= this.SetAvatar(profile, contact, client);

                    if (changed)
                    {
                        client.Submit();
                        this.UpdateTracker();
                    }
                }
                catch (XdbExecutionException ex)
                {
                    Log.Error($"Could not update the xConnect contact facets", ex, this);
                }
            }
        }

        private void UpdateTracker()
        {
            if (Tracker.Current?.Session == null)
            {
                return;
            }
            Tracker.Current.Session.Contact = this.contactManager.LoadContact(Tracker.Current.Session.Contact.ContactId);
        }

        private bool SetAvatar(UserProfile profile, XConnect.Contact contact, XConnectClient client)
        {
            var url = profile[Accounts.Constants.UserProfile.Fields.PictureUrl];
            var mimeType = profile[Accounts.Constants.UserProfile.Fields.PictureMimeType];
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(mimeType))
            {
                return false;
            }
            try
            {
                var web = new WebClient();
                var pictureData = web.DownloadData(url);
                var pictureMimeType = mimeType;

                var avatar = contact.Avatar();
                if (avatar == null)
                {
                    avatar = new Avatar(pictureMimeType, pictureData);
                }
                else
                {
                    avatar.MimeType = pictureMimeType;
                    avatar.Picture = pictureData;
                }
                client.SetFacet(contact, Avatar.DefaultFacetKey, avatar);
                return true;
            }
            catch (Exception exception)
            {
                Log.Warn($"Could not download profile picture {url}", exception, this);
                return false;
            }
        }

        private bool SetEmail(UserProfile profile, XConnect.Contact contact, XConnectClient client)
        {
            var email = profile.Email;
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            var emails = contact.Emails();
            if (emails == null)
            {
                emails = new EmailAddressList(new EmailAddress(email, false), null);
            }
            else
            {
                if (emails.PreferredEmail?.SmtpAddress == email)
                {
                    return false;
                }
                emails.PreferredEmail = new EmailAddress(email, false);
            }
            client.SetFacet(contact, EmailAddressList.DefaultFacetKey, emails);
            return true;
        }

        private bool SetPhone(UserProfile profile, XConnect.Contact contact, XConnectClient client)
        {
            var phoneNumber = profile[Accounts.Constants.UserProfile.Fields.PhoneNumber];
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }
            var phoneNumbers = contact.PhoneNumbers();
            if (phoneNumbers == null)
            {
                phoneNumbers = new PhoneNumberList(new PhoneNumber(null, phoneNumber), null);
            }
            else
            {
                if (phoneNumbers.PreferredPhoneNumber?.Number == phoneNumber)
                {
                    return false;
                }
                phoneNumbers.PreferredPhoneNumber = new PhoneNumber(null, phoneNumber);
            }
            client.SetFacet(contact, PhoneNumberList.DefaultFacetKey, phoneNumbers);
            return true;
        }

        private static bool SetPersonalInfo(UserProfile profile, XConnect.Contact contact, XConnectClient client)
        {
            var changed = false;

            var personalInfo = contact.Personal() ?? new PersonalInformation();
            changed |= SetBirthdate(profile, personalInfo);
            changed |= SetName(profile, personalInfo);
            changed |= SetGender(profile, personalInfo);
            changed |= SetLanguage(profile, personalInfo);
            if (!changed)
            {
                return false;
            }
            client.SetFacet(contact, PersonalInformation.DefaultFacetKey, personalInfo);
            return true;
        }

        private static bool SetLanguage(UserProfile profile, PersonalInformation personalInfo)
        {
            if (personalInfo.PreferredLanguage == profile[Accounts.Constants.UserProfile.Fields.Language])
            {
                return false;
            }
            personalInfo.PreferredLanguage = profile[Accounts.Constants.UserProfile.Fields.Language];
            return true;
        }

        private static bool SetGender(UserProfile profile, PersonalInformation personalInfo)
        {
            if (personalInfo.Gender == profile[Accounts.Constants.UserProfile.Fields.Gender])
            {
                return false;
            }
            personalInfo.Gender = profile[Accounts.Constants.UserProfile.Fields.Gender];
            return true;
        }

        private static bool SetName(UserProfile profile, PersonalInformation personalInfo)
        {
            var changed = false;
            if (personalInfo.FirstName != profile[Accounts.Constants.UserProfile.Fields.FirstName])
            {
                personalInfo.FirstName = profile[Accounts.Constants.UserProfile.Fields.FirstName];
                changed = true;
            }
            if (personalInfo.MiddleName != profile[Accounts.Constants.UserProfile.Fields.MiddleName])
            {
                personalInfo.MiddleName = profile[Accounts.Constants.UserProfile.Fields.MiddleName];
                changed = true;
            }
            if (personalInfo.LastName != profile[Accounts.Constants.UserProfile.Fields.LastName])
            {
                personalInfo.LastName = profile[Accounts.Constants.UserProfile.Fields.LastName];
                changed = true;
            }
            return changed;
        }

        private static bool SetBirthdate(UserProfile profile, PersonalInformation personalInfo)
        {
            var birthDateString = profile[Accounts.Constants.UserProfile.Fields.Birthday];
            if (string.IsNullOrEmpty(birthDateString))
            {
                return false;
            }
            DateTime birthDate;
            if (!DateTime.TryParse(birthDateString, out birthDate))
            {
                return false;
            }
            personalInfo.Birthdate = birthDate;
            return true;
        }

        //var personalInfo = this.contactProfileProvider.PersonalInfo;
        //personalInfo.FirstName = profile[Accounts.Constants.UserProfile.Fields.FirstName];
        //personalInfo.Surname = profile[Accounts.Constants.UserProfile.Fields.LastName];
        //this.SetPreferredPhoneNumber(profile[Accounts.Constants.UserProfile.Fields.PhoneNumber]);
        //    this.SetPreferredEmail(profile.Email);
        //    this.SetPicture(profile[Accounts.Constants.UserProfile.Fields.PictureUrl], profile[Accounts.Constants.UserProfile.Fields.PictureMimeType]);
        //this.SetTags(profile);
        //    this.contactProfileProvider.Flush();


        private Analytics.Model.Entities.ContactIdentifier GetContactId()
        {
            if (Tracker.Current?.Contact == null)
            {
                return null;
            }
            if (Tracker.Current.Contact.IsNew)
            {
                Tracker.Current.Contact.ContactSaveMode = ContactSaveMode.AlwaysSave;
                this.contactManager.SaveContactToCollectionDb(Tracker.Current.Contact);
            }
            return Tracker.Current.Contact.Identifiers.FirstOrDefault();
        }
    }
}