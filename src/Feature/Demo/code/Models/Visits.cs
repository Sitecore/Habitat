namespace Sitecore.Feature.Demo.Models
{
  using System.Collections.Generic;

  public class Visits
  {
    public int EngagementValue { get; set; }
    public IEnumerable<PageView> PageViews { get; set; }
    public int TotalPageViews { get; set; }
    public int TotalVisits { get; set; }
    public IEnumerable<EngagementPlanState> EngagementPlanStates { get; set; }
  }
}