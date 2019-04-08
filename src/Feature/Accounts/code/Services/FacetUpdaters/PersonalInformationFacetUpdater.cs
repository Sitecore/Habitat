namespace Sitecore.Feature.Accounts.Services.FacetUpdaters
{
    using System;
    using System.Collections.Generic;
    using Sitecore.Security;
    using Sitecore.XConnect;
    using Sitecore.XConnect.Collection.Model;

    public class PersonalInformationFacetUpdater : IContactFacetUpdater
    {
        public IList<string> FacetsToUpdate => new[] {PersonalInformation.DefaultFacetKey};

        public bool SetFacets(UserProfile profile, Contact contact, IXdbContext client)
        {
            var changed = false;
            var personalInfo = contact.GetFacet<PersonalInformation>(PersonalInformation.DefaultFacetKey) ?? new PersonalInformation();
            changed |= this.SetBirthdate(profile, personalInfo);
            changed |= this.SetName(profile, personalInfo);
            changed |= this.SetGender(profile, personalInfo);
            changed |= this.SetLanguage(profile, personalInfo);
            if (!changed)
            {
                return false;
            }
            client.SetFacet(contact, PersonalInformation.DefaultFacetKey, personalInfo);
            return true;
        }

        private string GetValue(UserProfile profile, string key)
        {
            var value = profile[key];
            return value != null && value.Trim().Length > 0 ? value : null;
        }

        private bool SetLanguage(UserProfile profile, PersonalInformation personalInfo)
        {
            var value = this.GetValue(profile, Accounts.Constants.UserProfile.Fields.Language);
            if (personalInfo.PreferredLanguage == value)
            {
                return false;
            }
            personalInfo.PreferredLanguage = value;
            return true;
        }

        private bool SetGender(UserProfile profile, PersonalInformation personalInfo)
        {
            var value = this.GetValue(profile, Accounts.Constants.UserProfile.Fields.Gender);
            if (personalInfo.Gender == value)
            {
                return false;
            }
            personalInfo.Gender = value;
            return true;
        }

        private bool SetName(UserProfile profile, PersonalInformation personalInfo)
        {
            var changed = false;
            var firstName = this.GetValue(profile, Accounts.Constants.UserProfile.Fields.FirstName);
            if (personalInfo.FirstName != firstName)
            {
                personalInfo.FirstName = firstName;
                changed = true;
            }
            var middleName = this.GetValue(profile, Accounts.Constants.UserProfile.Fields.MiddleName);
            if (personalInfo.MiddleName != middleName)
            {
                personalInfo.MiddleName = middleName;
                changed = true;
            }
            var lastName = this.GetValue(profile, Accounts.Constants.UserProfile.Fields.LastName);
            if (personalInfo.LastName != lastName)
            {
                personalInfo.LastName = lastName;
                changed = true;
            }
            return changed;
        }

        private bool SetBirthdate(UserProfile profile, PersonalInformation personalInfo)
        {
            DateTime? birthdate = null;
            var birthDateString = this.GetValue(profile, Accounts.Constants.UserProfile.Fields.Birthday);

            DateTime parsedDate;
            if (!string.IsNullOrEmpty(birthDateString) && DateTime.TryParse(birthDateString, out parsedDate))
            {
                birthdate = parsedDate;
            }

            if (personalInfo.Birthdate == birthdate)
            {
                return false;
            }
            personalInfo.Birthdate = birthdate;
            return true;
        }
    }
}