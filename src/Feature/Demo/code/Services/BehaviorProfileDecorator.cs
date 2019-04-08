namespace Sitecore.Feature.Demo.Services
{
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Analytics.Patterns;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Data;

  internal class BehaviorProfileDecorator : IProfileData
  {
    private readonly ProfileItem profile;
    private readonly IBehaviorProfileContext behaviorProfile;

    public BehaviorProfileDecorator(ProfileItem profile, IBehaviorProfileContext behaviorProfile)
    {
      this.profile = profile;
      this.behaviorProfile = behaviorProfile;
    }

    public IEnumerator<KeyValuePair<string, double>> GetEnumerator()
    {
      return this.GetScores().GetEnumerator();
    }

    private IEnumerable<KeyValuePair<string, double>> GetScores()
    {
      return this.behaviorProfile.Scores.Select(score => new KeyValuePair<string, double>(this.GetProfileKeyName(score.Key), score.Value));
    }

    private string GetProfileKeyName(ID key)
    {
      var profileKey = this.profile.Keys[key];
      return profileKey.KeyName;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetScores().GetEnumerator();
    }

    public int Count => this.behaviorProfile.NumberOfTimesScored;

    public double Total => (float)this.behaviorProfile.Total;

    public double this[string key]
    {
      get
      {
        var profileKey = this.profile.Keys[key];
        return profileKey == null ? 0 : this.behaviorProfile.GetScore(profileKey.ID);
      }
    }
  }
}