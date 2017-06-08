namespace Sitecore.Feature.Demo.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MongoDB.Driver;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Outcome;
    using Sitecore.Analytics.Outcome.Extensions;
    using Sitecore.Analytics.Outcome.Model;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Diagnostics;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Marketing.Definitions;
    using Sitecore.Marketing.Definitions.Outcomes.Model;
    using Sitecore.Marketing.Taxonomy;
    using Sitecore.Marketing.Taxonomy.Extensions;

    internal class OutcomeRepository
    {
        private readonly OutcomeManager outcomeManager;

        public OutcomeRepository(OutcomeManager outcomeManager)
        {
            this.outcomeManager = outcomeManager;
        }

        public OutcomeRepository() : this(Factory.CreateObject("outcome/outcomeManager", true) as OutcomeManager)
        {
        }

        public IEnumerable<Outcome> GetAll()
        {
            return this.GetCurrentOutcomes().Concat(this.GetHistoricalOutcomes()).Select(this.Create);
        }

        private Outcome Create(IOutcome outcome)
        {
            var definition = GetOutcomeDefinition(outcome.DefinitionId);
            return new Outcome
                   {
                       Title = definition?.Name ?? DictionaryPhraseRepository.Current.Get("/Demo/Outcomes/Unknown Outcome", "(Unknown)"),
                       Date = outcome.DateTime,
                       IsCurrentVisit = outcome.InteractionId?.ToGuid() == Tracker.Current?.Interaction.InteractionId,
                       OutcomeGroup = this.GetOutcomeGroup(definition)
                   };
        }

        private string GetOutcomeGroup(IOutcomeDefinition outcome)
        {
            if (outcome?.OutcomeGroupUri == null)
            {
                return null;
            }
            var outcomeGroupTaxonomyManager = TaxonomyManager.Provider.GetOutcomeGroupManager();
            var outcomeGroup = outcomeGroupTaxonomyManager.GetOutcomeGroup(outcome.OutcomeGroupUri, Context.Language.CultureInfo);
            return outcomeGroup == null ? null : outcomeGroupTaxonomyManager.GetFullName(outcomeGroup.Uri, "/");
        }

        private IEnumerable<IOutcome> GetCurrentOutcomes()
        {
            return Tracker.Current != null ? Tracker.Current.GetContactOutcomes() : Enumerable.Empty<IOutcome>();
        }

        private IEnumerable<IOutcome> GetHistoricalOutcomes()
        {
            try
            {
                return Tracker.Current != null ? this.outcomeManager.GetForEntity<IOutcome>(new ID(Tracker.Current.Contact.ContactId)).ToList() : Enumerable.Empty<IOutcome>();
            }
            //The OutcomeManager is not very error safe and will throw misc exceptions if the MongoDb database is offline
            catch (Exception exception)
            {
                Log.Debug($"GetHistoricalOutcomes failed. Mongo database is offline or reports an error: {exception.Message}", this);
                return Enumerable.Empty<IOutcome>();
            }
        }

        private static IOutcomeDefinition GetOutcomeDefinition(ID outcomeId)
        {
            var outcomes = DefinitionManagerFactory.Default.GetDefinitionManager<IOutcomeDefinition>();
            var outcome = outcomes.Get(outcomeId, Context.Language.CultureInfo);
            return outcome;
        }

        public IEnumerable<Outcome> GetLatest()
        {
            return this.GetAll().Take(10);
        }
    }
}