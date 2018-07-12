namespace Sitecore.Feature.Demo.Models
{
  using System.Collections.Generic;

  public class Profile
  {
    public string Name { get; set; }
    public IEnumerable<PatternMatch> PatternMatches { get; set; }
  }
}