namespace Sitecore.Feature.Accounts.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Feature.Accounts.Models;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Security;
    using Sitecore.Security.Accounts;
    using Sitecore.SecurityModel;

    [Service(typeof(IUserProfileService))]
    public class UserProfileService : IUserProfileService
    {
        private readonly IProfileSettingsService profileSettingsService;
        private readonly IUserProfileProvider userProfileProvider;
        private readonly IUpdateContactFacetsService updateContactFacetsService;
        private readonly IAccountTrackerService accountTrackerService;

        public UserProfileService(IProfileSettingsService profileSettingsService, IUserProfileProvider userProfileProvider, IUpdateContactFacetsService updateContactFacetsService, IAccountTrackerService accountTrackerService)
        {
            this.profileSettingsService = profileSettingsService;
            this.userProfileProvider = userProfileProvider;
            this.updateContactFacetsService = updateContactFacetsService;
            this.accountTrackerService = accountTrackerService;
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

        public virtual EditProfile GetProfile(User user)
        {
            this.SetProfileIfEmpty(user);

            var properties = this.userProfileProvider.GetCustomProperties(user.Profile);

            var model = new EditProfile
                        {
                            Email = user.Profile.Email,
                            FirstName = properties.ContainsKey(Constants.UserProfile.Fields.FirstName) ? properties[Constants.UserProfile.Fields.FirstName] : "",
                            LastName = properties.ContainsKey(Constants.UserProfile.Fields.LastName) ? properties[Constants.UserProfile.Fields.LastName] : "",
                            PhoneNumber = properties.ContainsKey(Constants.UserProfile.Fields.PhoneNumber) ? properties[Constants.UserProfile.Fields.PhoneNumber] : "",
                            Interest = properties.ContainsKey(Constants.UserProfile.Fields.Interest) ? properties[Constants.UserProfile.Fields.Interest] : "",
                            InterestTypes = this.profileSettingsService.GetInterests()
                        };

            return model;
        }

        public virtual void SaveProfile(UserProfile userProfile, EditProfile model)
        {
            var properties = new Dictionary<string, string>
                             {
                                 [Constants.UserProfile.Fields.FirstName] = model.FirstName,
                                 [Constants.UserProfile.Fields.LastName] = model.LastName,
                                 [Constants.UserProfile.Fields.PhoneNumber] = model.PhoneNumber,
                                 [Constants.UserProfile.Fields.Interest] = model.Interest,
                                 [nameof(userProfile.Name)] = model.FirstName,
                                 [nameof(userProfile.FullName)] = $"{model.FirstName} {model.LastName}".Trim()
                             };

            this.userProfileProvider.SetCustomProfile(userProfile, properties);
            this.updateContactFacetsService.UpdateContactFacets(userProfile);
            accountTrackerService.TrackEditProfile(userProfile);
        }

        public IEnumerable<string> GetInterests()
        {
            return this.profileSettingsService.GetInterests();
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