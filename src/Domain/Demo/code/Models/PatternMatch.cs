namespace Habitat.Demo.Models
{
  public class PatternMatch
  {
    public PatternMatch()
    {
    }

    public PatternMatch(string profile, string pattern, string image)
    {
      this.Profile = profile;
      this.PatternName = pattern;
      this.Image = image;
    }

    public string Profile { get; set; }
    public string PatternName { get; set; }
    public string Image { get; set; }
  }
}