namespace Habitat.Accounts.Services
{
  using System.Collections.Generic;
  using System.Web.Mvc;
  using Sitecore.Data.Items;
  using Sitecore.Security;

  public class UserProfileService : IUserProfileService
  {
    private readonly IProfileSettingsService profileSettingsService;
    private readonly IUserProfileProvider userProfileProvider;

    public UserProfileService(): this(new ProfileSettingsService(), new UserProfileProvider())
    {
    }

    public UserProfileService(IProfileSettingsService profileSettingsService, IUserProfileProvider userProfileProvider)
    {
      this.profileSettingsService = profileSettingsService;
      this.userProfileProvider = userProfileProvider;
    }

    public Item GetUserDefaultProfile()
    {
      return this.profileSettingsService.GetUserDefaultProfile();
    }

    public IEnumerable<string> GetInterests()
    {
      return this.profileSettingsService.GetInterests();
    }

    public IDictionary<string, string> GetProfile(UserProfile userProfile)
    {
      return this.userProfileProvider.GetCustomProperties(userProfile);
    }

    public ModelStateDictionary Validate(IDictionary<string, string> properties)
    {
      var processor = this.profileSettingsService.GetUserProfileProcessor();

      return processor.Validate(properties);
    }

    public void SetProfile(UserProfile userProfile, IDictionary<string, string> properties)
    {
      var processor = this.profileSettingsService.GetUserProfileProcessor();
      properties = processor.Process(properties);

      this.userProfileProvider.SetCustomProfile(userProfile, properties);
    }
  }
}