namespace Sitecore.Feature.Accounts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Feature.Accounts.Models;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Security;
    using Sitecore.Security.Accounts;
    using Sitecore.SecurityModel;

    public class UserProfileService : IUserProfileService
    {
        private readonly IProfileSettingsService profileSettingsService;
        private readonly IUserProfileProvider userProfileProvider;

        protected Item profile;
        protected virtual Item Profile => this.profile ?? (this.profile = this.profileSettingsService.GetUserDefaultProfile());
        protected virtual string FirstName => this.GetUserProfileFieldName(Templates.UserProfile.Fields.FirstName);
        protected virtual string LastName => this.GetUserProfileFieldName(Templates.UserProfile.Fields.LastName);
        protected virtual string PhoneNumber => this.GetUserProfileFieldName(Templates.UserProfile.Fields.PhoneNumber);
        protected virtual string Interest => this.GetUserProfileFieldName(Templates.UserProfile.Fields.Interest);

        public UserProfileService() : this(new ProfileSettingsService(), new UserProfileProvider())
        {
        }

        public UserProfileService(IProfileSettingsService profileSettingsService, IUserProfileProvider userProfileProvider)
        {
            this.profileSettingsService = profileSettingsService;
            this.userProfileProvider = userProfileProvider;
        }

        public virtual string GetUserDefaultProfileId()
        {
            return this.profileSettingsService.GetUserDefaultProfile()?.ID?.ToString();
        }

        private string GetUserProfileFieldName(ID fieldId)
        {
            using (new SecurityDisabler())
            {
                return this.Profile.Database.GetItem(fieldId).Name;
            }
        }

        public virtual EditProfile GetEmptyProfile()
        {
            return new EditProfile
                   {
                       InterestTypes = this.profileSettingsService.GetInterests()
                   };
        }

        public virtual EditProfile GetProfile(UserProfile userProfile)
        {
            var properties = this.userProfileProvider.GetCustomProperties(userProfile);

            var model = new EditProfile
                        {
                            Email = userProfile.Email,
                            FirstName = properties.ContainsKey(this.FirstName) ? properties[this.FirstName] : "",
                            LastName = properties.ContainsKey(this.LastName) ? properties[this.LastName] : "",
                            PhoneNumber = properties.ContainsKey(this.PhoneNumber) ? properties[this.PhoneNumber] : "",
                            Interest = properties.ContainsKey(this.Interest) ? properties[this.Interest] : "",
                            InterestTypes = this.profileSettingsService.GetInterests()
                        };

            return model;
        }

        public virtual void SetProfile(UserProfile userProfile, EditProfile model)
        {
            var properties = new Dictionary<string, string>
                             {
                                 [this.FirstName] = model.FirstName,
                                 [this.LastName] = model.LastName,
                                 [this.PhoneNumber] = model.PhoneNumber,
                                 [this.Interest] = model.Interest,
                                 [nameof(userProfile.Name)] = model.FirstName,
                                 [nameof(userProfile.FullName)] = $"{model.FirstName} {model.LastName}".Trim()
                             };

            this.userProfileProvider.SetCustomProfile(userProfile, properties);
        }

        public virtual bool ValidateProfile(EditProfile model, ModelStateDictionary modelState)
        {
            if (!this.profileSettingsService.GetInterests().Contains(model.Interest) && !string.IsNullOrEmpty(model.Interest))
            {
                modelState.AddModelError(nameof(model.Interest), DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/Interest Not Found", "Please select an interest from the list."));
            }

            return modelState.IsValid;
        }

        public IEnumerable<string> GetInterests()
        {
            return this.profileSettingsService.GetInterests();
        }

        public bool ValidateUser(User user)
        {
            if (!string.Equals(user.Domain.Name, Context.Site.Domain.Name, StringComparison.InvariantCultureIgnoreCase))
                return false;

            this.SetProfileIfEmpty(user);

            return this.GetUserDefaultProfileId() == user.Profile.ProfileItemId;
        }

        private void SetProfileIfEmpty(User user)
        {
            if (Context.User.Profile.ProfileItemId != null)
                return;

            user.Profile.ProfileItemId = this.GetUserDefaultProfileId();
            user.Profile.Save();
        }
    }
}