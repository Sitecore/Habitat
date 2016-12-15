namespace Sitecore.Feature.Accounts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Sitecore.Data.Items;
    using Sitecore.Feature.Accounts.Models;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Security;
    using Sitecore.Security.Accounts;

    public class UserProfileService : IUserProfileService
    {
        private readonly IProfileSettingsService profileSettingsService;
        private readonly IUserProfileProvider userProfileProvider;

        protected Item profile;
        protected virtual Item Profile => this.profile ?? (this.profile = this.profileSettingsService.GetUserDefaultProfile());
        protected virtual string FirstName => this.Profile.Database.GetItem(Templates.UserProfile.Fields.FirstName).Name;
        protected virtual string LastName => this.Profile.Database.GetItem(Templates.UserProfile.Fields.LastName).Name;
        protected virtual string PhoneNumber => this.Profile.Database.GetItem(Templates.UserProfile.Fields.PhoneNumber).Name;
        protected virtual string Interest => this.Profile.Database.GetItem(Templates.UserProfile.Fields.Interest).Name;

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

            var model = new EditProfile();
            if (properties.ContainsKey(this.FirstName))
            {
                model.FirstName = properties[this.FirstName];
            }
            if (properties.ContainsKey(this.LastName))
            {
                model.LastName = properties[this.LastName];
            }
            if (properties.ContainsKey(this.PhoneNumber))
            {
                model.PhoneNumber = properties[this.PhoneNumber];
            }
            if (properties.ContainsKey(this.Interest))
            {
                model.Interest = properties[this.Interest];
            }

            model.InterestTypes = this.profileSettingsService.GetInterests();

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