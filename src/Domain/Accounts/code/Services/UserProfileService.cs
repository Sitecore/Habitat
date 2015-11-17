namespace Habitat.Accounts.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Runtime.Remoting;
  using System.Web.Mvc;
  using Sitecore.Data.Items;
  using Sitecore.Security;

  public class UserProfileService : IUserProfileService
  {
    private readonly IProfileSettingsService profileSettingsService;
    private readonly IUserProfileProvider userProfileProvider;
    private IProfileProcessor profileProcessor;

    protected IProfileProcessor ProfileProcessor => this.profileProcessor ?? (this.profileProcessor = this.profileSettingsService.GetUserProfileProcessor());


    public UserProfileService(): this(new ProfileSettingsService(), new UserProfileProvider())
    {
    }

    public UserProfileService(IProfileSettingsService profileSettingsService, IUserProfileProvider userProfileProvider)
    {
      this.profileSettingsService = profileSettingsService;
      this.userProfileProvider = userProfileProvider;
    }

    public virtual Item GetUserDefaultProfile()
    {
      return this.profileSettingsService.GetUserDefaultProfile();
    }

    public virtual object GetEmptyProfile()
    {
      return this.ProfileProcessor.GetModel(new Dictionary<string, string>());
    }

    public virtual object GetProfile(UserProfile userProfile)
    {
      return this.ProfileProcessor.GetModel(this.userProfileProvider.GetCustomProperties(userProfile));
    }

    public virtual void SetProfile(UserProfile userProfile, object profileModel)
    {
      var properties = this.ProfileProcessor.GetProperties(profileModel);
       
      this.userProfileProvider.SetCustomProfile(userProfile, properties);
    }

    public virtual bool ValidateProfile(object profileModel, ModelStateDictionary modelState)
    {
      var validationResults = this.ProfileProcessor.ValidateModel(profileModel);

      if (validationResults != null && validationResults.Any())
      {
        foreach (var validationResult in validationResults)
        {
          foreach (var memberName in validationResult.MemberNames)
          {
            modelState.AddModelError(memberName, validationResult.ErrorMessage);
          }
        }

        return false;
      }

      return true;
    }
  }
}