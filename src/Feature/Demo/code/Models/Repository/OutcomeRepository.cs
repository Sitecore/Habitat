namespace Sitecore.Feature.Demo.Models.Repository
{
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Outcome;
  using Sitecore.Analytics.Outcome.Extensions;
  using Sitecore.Analytics.Outcome.Model;
  using Sitecore.Configuration;
  using Sitecore.Data;
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
               Title = definition?.Name ?? DictionaryRepository.Get("/Demo/Outcomes/UnknownOutcome", "(Unknown)"),
               Date = outcome.DateTime,
               IsCurrentVisit = outcome.InteractionId.ToGuid() == Tracker.Current.Interaction.InteractionId,
               OutcomeGroup = GetOutcomeGroup(definition)
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
      return Tracker.Current.GetContactOutcomes();
    }

    private IEnumerable<IOutcome> GetHistoricalOutcomes()
    {
      return this.outcomeManager.GetForEntity<IOutcome>(new ID(Tracker.Current.Contact.ContactId));
    }

    private static IOutcomeDefinition GetOutcomeDefinition(ID outcomeId)
    {
      var outcomes = DefinitionManagerFactory.Default.GetDefinitionManager<IOutcomeDefinition>();
      var outcome = outcomes.Get(outcomeId, Context.Language.CultureInfo);
      return outcome;
    }

    public IEnumerable<Outcome> GetLatest()
    {
      return GetAll().Take(10);
    }
  }
}