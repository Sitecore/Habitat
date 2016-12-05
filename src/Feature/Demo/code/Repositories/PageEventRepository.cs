namespace Sitecore.Feature.Demo.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Common;
    using Sitecore.Data;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Marketing.Definitions;
    using Sitecore.Marketing.Definitions.Goals;

    internal class PageEventRepository
    {
        public IEnumerable<PageEvent> GetGoals()
        {
            var current = this.GetCurrentGoals();
            var historic = this.GetHistoricGoals();

            return current.Union(historic);
        }

        public IEnumerable<PageEvent> GetPageEvents()
        {
            return this.GetCurrentPageEvents();
        }

        private IEnumerable<PageEvent> GetHistoricGoals()
        {
            Tracker.Current.Contact.LoadKeyBehaviorCache();
            var keyBehaviourCache = Tracker.Current.Contact.GetKeyBehaviorCache();
            foreach (var cachedGoal in keyBehaviourCache.Goals)
            {
                var goal = GetGoalDefinition(cachedGoal.Id.ToID());

                yield return new PageEvent
                             {
                                 Title = goal?.Name ?? DictionaryPhraseRepository.Current.Get("/Demo/Goals/Unknown Goal", "(Unknown)"),
                                 Date = cachedGoal.DateTime,
                                 EngagementValue = goal?.EngagementValuePoints ?? 0,
                                 IsCurrentVisit = false,
                                 Data = cachedGoal.Data
                             };
            }
        }

        private static IGoalDefinition GetGoalDefinition(ID goalId)
        {
            var goals = DefinitionManagerFactory.Default.GetDefinitionManager<IGoalDefinition>();
            var goal = goals.Get(goalId, Context.Language.CultureInfo);
            return goal;
        }

        private IEnumerable<PageEvent> GetCurrentGoals()
        {
            return Tracker.Current.Interaction.GetPages().SelectMany(page => page.PageEvents.Where(pe => pe.IsGoal)).Reverse().Select(this.Create);
        }

        private IEnumerable<PageEvent> GetCurrentPageEvents()
        {
            return Tracker.Current.Interaction.GetPages().SelectMany(page => page.PageEvents.Where(pe => !pe.IsGoal)).Reverse().Select(this.Create);
        }

        private PageEvent Create(PageEventData pageEventData)
        {
            return new PageEvent
                   {
                       IsCurrentVisit = true,
                       Date = pageEventData.DateTime,
                       EngagementValue = pageEventData.Value,
                       Title = pageEventData.Name,
                       Data = pageEventData.Text
                   };
        }

        public IEnumerable<PageEvent> GetLatest()
        {
            return this.GetGoals().Take(10);
        }
    }
}