namespace Habitat.Demo.Models
{
  public class PatternMatch
  {
    public PatternMatch()
    {
    }

    public PatternMatch(string profile, string pattern, string image)
    {
      Profile = profile;
      PatternName = pattern;
      Image = image;
    }

    public string Profile { get; set; }
    public string PatternName { get; set; }
    public string Image { get; set; }
  }
}