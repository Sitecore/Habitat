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

    public IEnumerator<KeyValuePair<string, float>> GetEnumerator()
    {
      return GetScores() as IEnumerator<KeyValuePair<string, float>>;
    }

    private IEnumerable<KeyValuePair<string, float>> GetScores()
    {
      return behaviorProfile.Scores.Select(score => new KeyValuePair<string, float>(GetProfileKeyName(score.Key), score.Value));
    }

    private string GetProfileKeyName(ID key)
    {
      var profileKey = profile.Keys[key];
      return profileKey.KeyName;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetScores() as IEnumerator<KeyValuePair<string, float>>;
    }

    public int Count
    {
      get
      {
        return behaviorProfile.NumberOfTimesScored;
      }
    }

    public float Total
    {
      get
      {
        return (float)behaviorProfile.Total;
      }
    }

    public float this[string key]
    {
      get
      {
        var profileKey = profile.Keys[key];
        return profileKey == null ? 0 : behaviorProfile.GetScore(profileKey.ID);
      }
    }
  }
}