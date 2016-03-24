namespace Sitecore.Feature.Demo.Models
{
  using System;

  public class PageView
  {
    public string Path { get; set; }
    public TimeSpan Duration { get; set; }
    public bool HasEngagementValue { get; set; }
    public bool HasMvTest { get; set; }
    public bool HasPersonalisation { get; set; }
  }
}