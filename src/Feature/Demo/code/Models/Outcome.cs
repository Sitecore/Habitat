namespace Sitecore.Feature.Demo.Models
{
  using System;

  public class Outcome
  {
    public string OutcomeGroup { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public bool IsCurrentVisit { get; set; }
  }
}