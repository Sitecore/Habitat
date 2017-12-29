namespace Sitecore.Foundation.Personalization.Rules.Actions
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Data;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Rules;
    using Sitecore.Rules.Actions;

    public class ApplyProfileValue<T> : RuleAction<T> where T : RuleContext
    {
        public string Profilecardkey { get; set; }

        public override void Apply(T ruleContext)
        {
            Assert.ArgumentNotNull(ruleContext, "ruleContext");
            Assert.IsNotNull(Tracker.Current.Session.Interaction, "Tracker interaction can not be null.");

            var profileItem = ruleContext.Item?.Database.GetItem(this.Profilecardkey);
            if (profileItem == null)
                return;

            ProcessProfile(profileItem, Tracker.Current.Session.Interaction);
        }

        private static void ProcessProfile(BaseItem profileItem, CurrentInteraction interaction)
        {
            if (profileItem?.Fields[Templates.ProfileCard.Fields.ProfileCardValue] == null) return;

            var trackingFields = new List<TrackingField>
            {
                new TrackingField(profileItem.Fields[Templates.ProfileCard.Fields.ProfileCardValue])
            };

            var fields = (IEnumerable<TrackingField>)trackingFields;

            TrackingFieldProcessor.ProcessProfiles(interaction, fields.FirstOrDefault());
        }
    }
}