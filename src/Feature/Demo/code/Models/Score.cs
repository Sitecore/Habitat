namespace Sitecore.Feature.Demo.Models
{
  using System;
  using System.Collections.Generic;
  using Sitecore.Data;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class Score
  {
    public Score(KeyValuePair<ID, float> x)
    {
      Id = x.Key.Guid;
      Value = x.Value;
      Name = x.Key.DisplayName();
    }

    public Guid Id { get; }
    public string Name { get; }
    public float Value { get; }

  }
}