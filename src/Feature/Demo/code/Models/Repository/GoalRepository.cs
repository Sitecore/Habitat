namespace Sitecore.Feature.Demo.Models.Repository
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Model;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Common;
  using Sitecore.Data;
  using Sitecore.Foundation.Dictionary.Repositories;
  using Sitecore.Marketing.Definitions;
  using Sitecore.Marketing.Definitions.Goals;

  internal class GoalRepository
  {
    public IEnumerable<Goal> GetAll()
    {
      var current = GetCurrent();
      var historic = GetHistoric();

      return current.Union(historic);
    }

    private IEnumerable<Goal> GetHistoric()
    {
      var keyBehaviourCache = Tracker.Current.Contact.GetKeyBehaviorCache();
      foreach (var cachedGoal in keyBehaviourCache.Goals)
      {
        var goal = GetGoalDefinition(cachedGoal.Id.ToID());

        yield return new Goal
                     {
                       Title = goal?.Name ?? DictionaryRepository.Get("/Demo/Goals/UnknownGoal", "(Unknown)"),
                       Date = cachedGoal.DateTime,
                       EngagementValue = goal?.EngagementValuePoints ?? 0,
                       IsCurrentVisit = false
                     };
      }
    }

    private static IGoalDefinition GetGoalDefinition(ID goalId)
    {
      var goals = DefinitionManagerFactory.Default.GetDefinitionManager<IGoalDefinition>();
      var goal = goals.Get(goalId, Context.Language.CultureInfo);
      return goal;
    }

    private IEnumerable<Goal> GetCurrent()
    {
      return Tracker.Current.Interaction.GetPages().SelectMany(page => page.PageEvents.Where(pe => pe.IsGoal)).Reverse().Select(Create);
    }

    private Goal Create(PageEventData pageEventData)
    {
      return new Goal
             {
               IsCurrentVisit = true,
               Date = pageEventData.DateTime,
               EngagementValue = pageEventData.Value,
               Title = pageEventData.Name
             };
    }

    public IEnumerable<Goal> GetLatest()
    {
      return GetAll().Take(10);
    }
  }
}