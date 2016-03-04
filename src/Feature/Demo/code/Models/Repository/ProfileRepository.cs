namespace Sitecore.Feature.Demo.Models.Repository
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Feature.Demo.Services;

  internal class ProfileRepository
  {
    private readonly IProfileProvider profileProvider;

    public ProfileRepository(IProfileProvider profileProvider)
    {
      this.profileProvider = profileProvider;
    }

    public IEnumerable<Profile> GetSiteProfiles()
    {
      if (!Tracker.IsActive)
      {
        return Enumerable.Empty<Profile>();
      }

      return profileProvider.GetSiteProfiles().Where(profileProvider.HasMatchingPattern).Select(x => new Profile { Name = x.NameField, PatternMatches = profileProvider.GetPatternsWithGravityShare(x) });
    }
  }
}