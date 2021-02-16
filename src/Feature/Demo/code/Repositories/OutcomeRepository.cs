namespace Sitecore.Feature.Demo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Marketing.Definitions;
    using Sitecore.Marketing.Definitions.Outcomes.Model;
    using Sitecore.Marketing.Taxonomy;
    using Sitecore.Marketing.Taxonomy.Extensions;

    [Service]
    public class OutcomeRepository
    {
        private readonly IDefinitionManager<IOutcomeDefinition> outcomeDefinitionManager;
        private readonly IOutcomeGroupTaxonomyManager outcomeGroupTaxonomyManager;

        public OutcomeRepository(IDefinitionManager<IOutcomeDefinition> outcomeDefinitionManager, ITaxonomyManagerProvider taxonomyManagerProvider)
        {
            this.outcomeDefinitionManager = outcomeDefinitionManager;
            this.outcomeGroupTaxonomyManager = taxonomyManagerProvider.GetOutcomeGroupManager();
        }

        public IEnumerable<Outcome> GetAll()
        {
            return this.GetCurrentOutcomes().Concat(this.GetHistoricalOutcomes());
        }

        private Outcome Create(Guid outcomeDefinitionId, DateTime timeStamp, bool currentInteraction)
        {
            var definition = GetOutcomeDefinition(outcomeDefinitionId);
            return new Outcome
            {
                Title = definition?.Name ?? DictionaryPhraseRepository.Current.Get("/Demo/Outcomes/Unknown Outcome", "(Unknown)"),
                Date = timeStamp,
                IsCurrentVisit = currentInteraction,
                OutcomeGroup = this.GetOutcomeGroup(definition)
            };
        }

        private string GetOutcomeGroup(IOutcomeDefinition outcome)
        {
            if (outcome?.Id == null)
            {
                return null;
            }
            var outcomeGroup = this.outcomeGroupTaxonomyManager.GetOutcomeGroup(outcome.OutcomeGroupUri, Context.Language.CultureInfo);
            return outcomeGroup == null ? null : this.outcomeGroupTaxonomyManager.GetFullName(outcomeGroup.Uri, "/");
        }

        private IEnumerable<Outcome> GetCurrentOutcomes()
        {
            var interactionOutcomes = Tracker.Current?.Interaction?.Outcomes ?? Enumerable.Empty<OutcomeData>();
            var pageOutcomes = Tracker.Current?.Interaction?.Pages.SelectMany(p => p.Outcomes) ?? Enumerable.Empty<OutcomeData>();
            var allOutcomes = interactionOutcomes.Union(pageOutcomes).OrderByDescending(o => o.Timestamp);
            return allOutcomes.Select(o => this.Create(o.OutcomeDefinitionId, o.Timestamp, true));
        }

        private IEnumerable<Outcome> GetHistoricalOutcomes()
        {
            var outcomes = Tracker.Current.Contact.KeyBehaviorCache.Outcomes;
            foreach (var outcome in outcomes)
            {
                yield return this.Create(outcome.Id, outcome.DateTime, false);
            }
        }

        private IOutcomeDefinition GetOutcomeDefinition(Guid outcomeId)
        {
            return outcomeDefinitionManager.Get(outcomeId, Context.Language.CultureInfo) ?? outcomeDefinitionManager.Get(outcomeId, CultureInfo.InvariantCulture);
        }

        public IEnumerable<Outcome> GetLatest()
        {
            return this.GetAll().Take(10);
        }
    }
}