namespace Sitecore.Feature.Demo.Models
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;

  public class BehaviorProfile
  {
    private readonly IBehaviorProfileContext behaviorProfileContext;

    public BehaviorProfile(IBehaviorProfileContext behaviorProfileContext)
    {
      this.behaviorProfileContext = behaviorProfileContext;
    }


    public Guid Id => behaviorProfileContext.Id.Guid;
    public string Name => behaviorProfileContext.Id.DisplayName();


    public int NumberOfTimesScored => behaviorProfileContext.NumberOfTimesScored;
    public IEnumerable<Score> Scores => behaviorProfileContext.Scores.Select(x => new Score(x));
  }
}