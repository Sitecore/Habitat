namespace Sitecore.Foundation.SitecoreExtensions.Services
{
    using System;
    using System.Globalization;
    using Sitecore.Analytics;
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Marketing.Definitions;
    using Sitecore.Marketing.Definitions.Goals;
    using Sitecore.Marketing.Definitions.Outcomes.Model;
    using Sitecore.Marketing.Definitions.PageEvents;

    [Service(typeof(ITrackerService), Lifetime = Lifetime.Transient)]
    public class TrackerService : ITrackerService
    {
        public TrackerService(IDefinitionManager<IPageEventDefinition> pageEventDefinitionManager, IDefinitionManager<IOutcomeDefinition> outcomeDefinitionManager, IDefinitionManager<IGoalDefinition> goalDefinitionManager)
        {
            this.PageEventDefinitionManager = pageEventDefinitionManager;
            this.OutcomeDefinitionManager = outcomeDefinitionManager;
            this.GoalDefinitionManager = goalDefinitionManager;
        }

        public IDefinitionManager<IGoalDefinition> GoalDefinitionManager { get; }
        public IDefinitionManager<IPageEventDefinition> PageEventDefinitionManager { get; }
        public IDefinitionManager<IOutcomeDefinition> OutcomeDefinitionManager { get; }


        public bool IsActive => Tracker.Enabled && Tracker.Current != null && Tracker.Current.IsActive;

        public virtual void TrackPageEvent(Guid pageEventId, string text = null, string data = null, string dataKey = null, int? value = null)
        {
            Assert.ArgumentNotNull(pageEventId, nameof(pageEventId));
            if (!this.IsActive)
            {
                return;
            }

            var pageEventDefinition = this.PageEventDefinitionManager.Get(pageEventId, CultureInfo.InvariantCulture);
            if (pageEventDefinition == null)
            {
                Log.Warn($"Cannot find page event: {pageEventId}", this);
                return;
            }

            var eventData = Tracker.Current.CurrentPage.RegisterPageEvent(pageEventDefinition);
            if (data != null)
            {
                eventData.Data = data;
            }
            if (dataKey != null)
            {
                eventData.DataKey = dataKey;
            }
            if (text != null)
            {
                eventData.Text = text;
            }
            if (value != null)
            {
                eventData.Value = value.Value;
            }
        }

        public void TrackGoal(Guid goalId, string text = null, string data = null, string dataKey = null, int? value = null)
        {
            Assert.ArgumentNotNull(goalId, nameof(goalId));
            if (!this.IsActive)
            {
                return;
            }

            var goalDefinition = this.GoalDefinitionManager.Get(goalId, CultureInfo.InvariantCulture);
            if (goalDefinition == null)
            {
                Log.Warn($"Cannot find goal: {goalId}", this);
                return;
            }

            var eventData = Tracker.Current.CurrentPage.RegisterGoal(goalDefinition);
            if (data != null)
            {
                eventData.Data = data;
            }
            if (dataKey != null)
            {
                eventData.DataKey = dataKey;
            }
            if (text != null)
            {
                eventData.Text = text;
            }
            if (value != null)
            {
                eventData.Value = value.Value;
            }
        }

        public void TrackOutcome(Guid outComeDefinitionId)
        {
            Assert.ArgumentNotNull(outComeDefinitionId, nameof(outComeDefinitionId));

            if (!this.IsActive || Tracker.Current.Contact == null)
            {
                return;
            }

            var outcomeDefinition = this.OutcomeDefinitionManager.Get(outComeDefinitionId, CultureInfo.InvariantCulture);
            if (outcomeDefinition == null)
            {
                Log.Warn($"Cannot find outcome: {outComeDefinitionId}", this);
                return;
            }
            Tracker.Current.CurrentPage.RegisterOutcome(outcomeDefinition, "USD", 0);
        }


        public void IdentifyContact(string source, string identifier)
        {
            if (!this.IsActive)
            {
                return;
            }

            Tracker.Current.Session.IdentifyAs(source, identifier);
        }
    }
}