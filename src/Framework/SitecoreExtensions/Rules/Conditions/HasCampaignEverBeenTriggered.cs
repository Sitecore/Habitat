namespace Habitat.Framework.SitecoreExtensions.Rules.Conditions
{
    using System;
    using System.Linq;

    using Sitecore.Analytics;
    using Sitecore.Analytics.Data;
    using Sitecore.Rules;
    using Sitecore.Rules.Conditions;

    public class HasCampaignEverTriggered<T> : WhenCondition<T> where T : RuleContext
    {
        public string CampaignId { get; set; }

        protected override bool Execute(T ruleContext)
        {
            Tracker.Current.Contact.LoadHistorycalData(4);
            return Tracker.Current.Interaction.GetPages().Any(x => x.PageEvents.FirstOrDefault(y => string.Equals(y.Data, this.CampaignId, StringComparison.InvariantCultureIgnoreCase)) != null);
        }
    }
}