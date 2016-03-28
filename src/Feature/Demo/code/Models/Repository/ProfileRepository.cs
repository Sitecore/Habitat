namespace Sitecore.Feature.Demo.Models.Repository
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Feature.Demo.Services;

  internal class ProfileRepository
  {
    private readonly IProfileProvider profileProvider;

    public ProfileRepository(IProfileProvider profileProvider)
    {
      this.profileProvider = profileProvider;
    }

    public IEnumerable<Profile> GetProfiles(ProfilingTypes profiling)
    {
      if (!Tracker.IsActive)
      {
        return Enumerable.Empty<Profile>();
      }

      return profileProvider.GetSiteProfiles().Where(p => profileProvider.HasMatchingPattern(p, profiling)).Select(x => new Profile { Name = x.NameField, PatternMatches = profileProvider.GetPatternsWithGravityShare(x, profiling) });
    }
  }

}