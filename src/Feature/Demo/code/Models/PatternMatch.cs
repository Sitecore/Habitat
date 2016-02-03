namespace Sitecore.Feature.Demo.Models
{
  using System.Collections.Generic;

  public class Profile
  {
    public string Name { get; set; }
    public IEnumerable<PatternMatch> PatternMatches { get; set; }
  }

  public class PatternMatch
  {
    public PatternMatch()
    {
    }

    public PatternMatch(string profile, string pattern, string image, double matchPercentage)
    {
      this.Profile = profile;
      this.PatternName = pattern;
      this.Image = image;
      this.MatchPercentage = matchPercentage;
    }

    public string Profile { get; set; }
    public string PatternName { get; set; }
    public string Image { get; set; }
    public double MatchPercentage { get; set; }
  }
}