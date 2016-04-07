namespace Sitecore.Feature.Demo.Services
{
  using System.Collections.Generic;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Feature.Demo.Models;

  public enum ProfilingTypes
  {
    Active,
    Historic
  }

  public interface IProfileProvider
  {
    IEnumerable<ProfileItem> GetSiteProfiles();
    bool HasMatchingPattern(ProfileItem currentProfile, ProfilingTypes type);
    IEnumerable<PatternMatch> GetPatternsWithGravityShare(ProfileItem visibleProfile, ProfilingTypes type);
  }
}