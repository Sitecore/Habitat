namespace Sitecore.Feature.Demo.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Runtime.CompilerServices;
  using System.Web;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Tracking;
  using AnalyticsProfile = Analytics.Tracking.Profile;
  public class ExperienceProfile
  {
    private readonly AnalyticsProfile profile;

    public ExperienceProfile(AnalyticsProfile profiles)
    {
      this.profile = profiles;
    }

    public string Name => profile.ProfileName;

    public IEnumerable<KeyValuePair<string, float>> Scores => this.profile;
  }
}