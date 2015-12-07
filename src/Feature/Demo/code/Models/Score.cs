namespace Sitecore.Feature.Demo.Models
{
  using System;
  using System.Collections.Generic;
  using Sitecore.Data;

  public class Score
  {
    private KeyValuePair<ID, float> x;

    public Score(KeyValuePair<ID, float> x)
    {
      Id = x.Key.Guid;
      Value = x.Value;
      Name = Context.Database.GetItem(x.Key).Name;
    }

    public Guid Id { get; }
    public string Name { get; }
    public float Value { get; }
  }
}