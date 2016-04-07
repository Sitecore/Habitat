namespace Sitecore.Feature.Demo.Models.Repository
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Automation.Data;
  using Sitecore.Diagnostics;

  internal class EngagementPlanStateRepository
  {
    public IEnumerable<EngagementPlanState> GetCurrent()
    {
      try
      {
        var automationStateManager = AutomationStateManager.Create(Tracker.Current.Contact);
        var engagementStates = automationStateManager.GetAutomationStates().ToArray();

        return engagementStates.Select(stateContext => new EngagementPlanState
        {
          EngagementPlanTitle = stateContext.PlanItem.DisplayName,
          Title = stateContext.StateItem.DisplayName,
          Date = stateContext.EntryDateTime
        }).ToArray();
      }
      catch (Exception ex)
      {
        Log.Error("VisitInformation: Could not load engagement states", ex);
        return Enumerable.Empty<EngagementPlanState>();
      }
    }
  }
}